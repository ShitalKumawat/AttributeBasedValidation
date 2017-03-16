using Model;
using Prism.Mvvm;

namespace AttributeBasedValidation
{
    public class MainViewModel : BindableBase
    {
        private Employee _employee;
        public Employee Employee
        {
            get { return _employee; }
            set
            {
                SetProperty(ref this._employee, value);
            }
        }

        public MainViewModel()
        {
            Employee = new Employee();
        }

    }
}
