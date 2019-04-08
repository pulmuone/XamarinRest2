using DevExpress.Mobile.DataGrid;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinJson.Controls;
using XamarinJson.Helpers;
using XamarinJson.Models;
using XamarinJson.Services;

namespace XamarinJson.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Employee2> _employeeList2 = new ObservableCollection<Employee2>();
        private ObservableRangeCollection<Employee2> _employeeList = new ObservableRangeCollection<Employee2>();
        public ICommand LoginCommand { get; } //set을 안두는 이유는 생성자에서만 사용하기 때문에, 생성자 이외의 곳에서 사용할 경우 private set; 추가
        public ICommand SearchCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ListViewTappedCommand { get; }

        public ICommand GridRowDoubleTapCommand { get; }

        public ICommand GridRowTapCommand { get; }

        public ICommand EntryCompletedCommand { get; }

        private Employee _employee = new Employee();

        private string _routeCode = string.Empty;

        private Employee _selectedData = new Employee();

        private int _selectedRow;

        List<Employee2> _lstEmp = new List<Employee2>();

        public MainViewModel()
        {
            LoginCommand = new Command(async() => await Login());

            SearchCommand = new Command(
                execute: async () =>
                {
                    IsBusy = true;
                    IsEnabled = false;
                    RefreshCanExecutesAsync();
                    //ToDo
                    await Search();

                    IsEnabled = true;
                    RefreshCanExecutesAsync();
                    IsBusy = false;
                },
                canExecute: () =>
                {
                    return IsEnabled;
                });
            

            DeleteCommand = new Command(() => DeleteAsync());
            ListViewTappedCommand = new Command<SelectedItemChangedEventArgs>((obj) => ListViewTapped(obj));
            EntryCompletedCommand = new Command<Entry>((obj) => EntryRouteCode(obj));

            GridRowDoubleTapCommand = new Command<DevExpress.Mobile.DataGrid.RowDoubleTapEventArgs>((e) => GridDoubleRowTap(e));
            GridRowTapCommand = new Command<DevExpress.Mobile.DataGrid.RowTapEventArgs>((e) => GridRowTap(e));

            for (int i = 0; i < 1000; i++)
            {
                _lstEmp.Add(new Employee2(i, "홍길동", 123456789, "개발자", "개발자", "개발자", "개발자"));
            }            
        }

        private void RefreshCanExecutesAsync()
        {
            (SearchCommand as Command).ChangeCanExecute();
        }

        private void GridRowTap(RowTapEventArgs e)
        {
            Debug.WriteLine(e.RowHandle);
            Debug.WriteLine(e.FieldName);

            //Debug.WriteLine(SelectedEmployee.Ename);
        }

        private void GridDoubleRowTap(DevExpress.Mobile.DataGrid.RowDoubleTapEventArgs e)
        {
            Console.WriteLine(SelectedData.Ename);
            Console.WriteLine(SelectedRow);

            //SelectedData = null;
            //SelectedRow = -1;
        }

        private void EntryRouteCode(object obj)
        {
            Debug.WriteLine("test");

            Debug.WriteLine(RouteCode);

            Debug.WriteLine(((Entry)obj).Text);
            
        }

        private void ListViewTapped(SelectedItemChangedEventArgs obj)
        {
            _employee = obj.SelectedItem as Employee;            
        }

        private void DeleteAsync()
        {
            //EmployeeList.Clear();

            EmployeeList2 = null;
            EmployeeList2 = new ObservableCollection<Employee2>();

            //if(_employee != null)
            //{
            //    await ResourceService.GetInstance().DeleteResouce<Employee>(_employee.Empno);
            //}
        }

        private async Task Search()
        {
            //ActivityIndicator 볼려고 3초 기다림
            await Task.Delay(3000);

            string responseResult = string.Empty;
            string requestParamJson = string.Empty;

            //바인딩 시간 체크
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //EmployeeList2 = new ObservableCollection<Employee>(await ResourceService.GetInstance().GetResources<Employee>());

            //시간 : 0.009, 0.0001, 0.0001, 0.0001, 0.0001
            EmployeeList2 = null;
            EmployeeList2 = new ObservableCollection<Employee2>(_lstEmp);

            //시간 : 0.13, 0.08, 0.05, 0.04, 0.06
            //EmployeeList.Clear();
            //EmployeeList.AddRange(_lstEmp, System.Collections.Specialized.NotifyCollectionChangedAction.Reset);

            //EmployeeList.AddRange(await ResourceService.GetInstance().GetResources<Employee2>(), System.Collections.Specialized.NotifyCollectionChangedAction.Reset);

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }

        private async Task Login()
        {
            Settings.AuthToken = await BaseHttpService.Instance.AuthorizationAsync("admin", "1234");

            Debug.WriteLine(Settings.AuthToken);
            
        }

        public string RouteCode { get => _routeCode; set => SetProperty(ref _routeCode, value); }

        
        public int SelectedRow { get => _selectedRow; set => SetProperty(ref _selectedRow, value); }
        public Employee SelectedData { get => _selectedData; set => SetProperty(ref _selectedData, value); }

        public ObservableRangeCollection<Employee2> EmployeeList { get => _employeeList; set => SetProperty(ref this._employeeList, value); }

        public ObservableCollection<Employee2> EmployeeList2 { get => _employeeList2; set => SetProperty(ref this._employeeList2, value); }
    }
}
