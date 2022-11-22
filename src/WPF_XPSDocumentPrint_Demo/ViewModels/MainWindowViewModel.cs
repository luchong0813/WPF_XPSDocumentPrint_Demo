using Prism.Mvvm;

using System.Windows.Controls;
using System.Windows.Documents;
using System;
using System.Windows;
using System.Windows.Threading;
using System.IO.Packaging;
using System.IO;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.Threading;

namespace WPF_XPSDocumentPrint_Demo.ViewModels
{
    public class MainWindowViewModel : BindableBase, IDisposable
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private FixedDocumentSequence _StudentDocument;
        /// <summary>
        /// 模板文档（绑定至前端要显示的文档视图中）
        /// </summary>
        public FixedDocumentSequence StudentDocument
        {
            get { return _StudentDocument; }
            set { _StudentDocument = value; RaisePropertyChanged(); }
        }


        private delegate void LoadXpsMethod();
        private MemoryStream ms;
        private Package package;
        private Uri DocumentUri;

        public MainWindowViewModel()
        {
            var doc = FillFlowDocument();
            Application.Current.Dispatcher.BeginInvoke(
                new LoadXpsMethod(() => FillDocumentViewer(doc)), DispatcherPriority.ApplicationIdle);
        }


        private void FillDocumentViewer(FlowDocument doc)
        {
            try
            {
                ms = new MemoryStream();
                package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
                DocumentUri = new Uri("pack://InMemoryDocument.xps");
                PackageStore.AddPackage(DocumentUri, package);
                XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Fast, DocumentUri.AbsoluteUri);
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
                writer.Write(((IDocumentPaginatorSource)doc).DocumentPaginator);
                StudentDocument = xpsDocument.GetFixedDocumentSequence();
                xpsDocument.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("读取打印模板错误：{0}", e.ToString());
            }
        }

        private FlowDocument FillFlowDocument()
        {
            var doc = (FlowDocument)Application.LoadComponent(new Uri("/WPF_XPSDocumentPrint_Demo;component/Resources/StudentTemplate.xaml", UriKind.RelativeOrAbsolute));
            doc.DataContext = new Models.Student
            {
                Name = "傲慢与偏见",
                Class = "三年一班",
                Chinese = 99,
                Math = 59,
                English = 66,
                Comments = "此乃难得一见的奇才，未来可期！！！（Tips:这文字是绑定的，不是静态数据）"
            };
            return doc;
        }

        public void Dispose()
        {
            PackageStore.RemovePackage(DocumentUri);
            if (package != null)
            {
                package.Close();
                package = null;
            }
            if (ms != null)
            {
                ms.Close();
                ms = null;
            }
            DocumentUri = null;
        }
    }
}
