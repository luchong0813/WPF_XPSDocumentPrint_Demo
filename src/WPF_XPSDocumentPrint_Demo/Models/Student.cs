using Prism.Mvvm;

namespace WPF_XPSDocumentPrint_Demo.Models
{
    public class Student : BindableBase
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; RaisePropertyChanged(); }
        }


        private string _Class;

        public string Class
        {
            get { return _Class; }
            set { _Class = value; RaisePropertyChanged(); }
        }


        private int _Chinese;

        public int Chinese
        {
            get { return _Chinese; }
            set { _Chinese = value; RaisePropertyChanged(); }
        }


        private int _Math;

        public int Math
        {
            get { return _Math; }
            set { _Math = value; RaisePropertyChanged(); }
        }


        private int _English;

        public int English
        {
            get { return _English; }
            set { _English = value; RaisePropertyChanged(); }
        }


        private string _Comments;

        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; RaisePropertyChanged(); }
        }

    }
}
