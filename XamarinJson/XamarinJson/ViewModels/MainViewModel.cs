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


        //private ObservableCollection<Employee> _employeeList2= new ObservableCollection<Employee>();
        private ObservableRangeCollection<Employee> _employeeList = new ObservableRangeCollection<Employee>();
        public ICommand LoginCommand { get; } //set을 안두는 이유는 생성자에서만 사용하기 때문에, 생성자 이외의 곳에서 사용할 경우 private set; 추가
        public ICommand SearchCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ListViewTappedCommand { get; }

        public ICommand EntryCompletedCommand { get; }

        private Employee _employee = new Employee();
        public MainViewModel()
        {
            LoginCommand = new Command(async() => await Login());
            SearchCommand = new Command(async () => await Search());
            DeleteCommand = new Command(async () => await DeleteAsync());
            ListViewTappedCommand = new Command<SelectedItemChangedEventArgs>((obj) => ListViewTapped(obj));
            EntryCompletedCommand = new Command(() => EntryRouteCode());
        }

        private void EntryRouteCode()
        {
            Debug.WriteLine("test");
        }

        private void ListViewTapped(SelectedItemChangedEventArgs obj)
        {
            _employee = obj.SelectedItem as Employee;            
        }

        private async Task DeleteAsync()
        {
            if(_employee != null)
            {
                await ResourceService.GetInstance().DeleteResouce<Employee>(_employee.Empno);
            }
        }

        private async Task Search()
        {
            string responseResult = string.Empty;
            string requestParamJson = string.Empty;

            EmployeeList.Clear();
            EmployeeList.AddRange(await ResourceService.GetInstance().GetResources<Employee>(), System.Collections.Specialized.NotifyCollectionChangedAction.Reset);
        }

        private async Task Login()
        {
            Settings.AuthToken = await BaseHttpService.Instance.AuthorizationAsync("admin", "1234");

            Debug.WriteLine(Settings.AuthToken);
        }

        public ObservableRangeCollection<Employee> EmployeeList { get => _employeeList; set => SetProperty(ref this._employeeList, value); }
    }
}
