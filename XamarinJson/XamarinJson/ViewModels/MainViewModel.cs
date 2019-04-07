using DevExpress.Mobile.DataGrid;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
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
        private ObservableCollection<Employee> _employeeList2 = new ObservableCollection<Employee>();
        private ObservableRangeCollection<Employee> _employeeList = new ObservableRangeCollection<Employee>();
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

        public MainViewModel()
        {
            LoginCommand = new Command(async() => await Login());
            SearchCommand = new Command(async () => await Search());
            DeleteCommand = new Command(async () => await DeleteAsync());
            ListViewTappedCommand = new Command<SelectedItemChangedEventArgs>((obj) => ListViewTapped(obj));
            EntryCompletedCommand = new Command<Entry>((obj) => EntryRouteCode(obj));

            GridRowDoubleTapCommand = new Command<DevExpress.Mobile.DataGrid.RowDoubleTapEventArgs>((e) => GridDoubleRowTap(e));
            GridRowTapCommand = new Command<DevExpress.Mobile.DataGrid.RowTapEventArgs>((e) => GridRowTap(e));
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

        private async Task DeleteAsync()
        {
            //if(_employee != null)
            //{
            //    await ResourceService.GetInstance().DeleteResouce<Employee>(_employee.Empno);
            //}
        }

        private async Task Search()
        {
            string responseResult = string.Empty;
            string requestParamJson = string.Empty;

            //EmployeeList2 = new ObservableCollection<Employee>(await ResourceService.GetInstance().GetResources<Employee>());

            List<Employee> lstEmp = new List<Employee>();

            lstEmp.Add(new Employee(1, "홍길동", 1000, "개발자"));
            lstEmp.Add(new Employee(2, "박찬호", 2000, "야구"));
            lstEmp.Add(new Employee(3, "이순신", 3000, "군인"));

            EmployeeList.Clear();
            EmployeeList.AddRange(lstEmp, System.Collections.Specialized.NotifyCollectionChangedAction.Reset);
            //EmployeeList.AddRange(await ResourceService.GetInstance().GetResources<Employee>(), System.Collections.Specialized.NotifyCollectionChangedAction.Reset);
        }

        private async Task Login()
        {
            Settings.AuthToken = await BaseHttpService.Instance.AuthorizationAsync("admin", "1234");

            Debug.WriteLine(Settings.AuthToken);
            
        }

        public string RouteCode { get => _routeCode; set => SetProperty(ref _routeCode, value); }

        
        public int SelectedRow { get => _selectedRow; set => SetProperty(ref _selectedRow, value); }
        public Employee SelectedData { get => _selectedData; set => SetProperty(ref _selectedData, value); }

        public ObservableRangeCollection<Employee> EmployeeList { get => _employeeList; set => SetProperty(ref this._employeeList, value); }

        public ObservableCollection<Employee> EmployeeList2 { get => _employeeList2; set => SetProperty(ref this._employeeList2, value); }
    }
}
