using System.ComponentModel;
using System.Diagnostics;

namespace InvestmentTrackerUI.ModelViews
{
    public class ViewModelBase
    {
        public static bool ThrowOnInvalidPropertyName;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise the PropertyChanged event
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            if(TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name " + propertyName;
                if(ThrowOnInvalidPropertyName)
                {
                    throw new System.Exception(msg);
                }
                else
                    Debug.Fail(msg);
            }
        }
    }
}