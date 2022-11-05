using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using core.model.node;
using core.service;
using lab03directory_scanner.view;
using lab03directory_scanner.view.node;
using UI.VM;

namespace lab03directory_scanner.VM
{
	public class ApplicationViewModel : INotifyPropertyChanged
	{
		private bool _isSearchEnabled;
		public bool IsSearchEnabled
		{
			get
			{
				return _isSearchEnabled;
			}
			set
			{
				_isSearchEnabled = value;
				OnPropertyChanged("IsStartEnable");
				OnPropertyChanged("IsSearchEnabled");
			}
		}

		private bool _isFileChosen;
		public bool IsFileChosen
		{
			get
			{
				return _isFileChosen;
			}
			set
			{
				_isFileChosen = value;
				OnPropertyChanged("IsStartEnable");
				OnPropertyChanged("IsFileChosen");
			}
		}

		public bool IsStartEnable
		{
			get
			{
				return IsFileChosen && !IsSearchEnabled;
			}
		}

		private string? _filePath;
		public string FilePath
		{
			get
			{
				return _filePath ?? "";
			}
			set
			{
				_filePath = value;
				OnPropertyChanged("FilePath");
			}
		}

		private IScanner _scanner;
		private ObservableCollection<NodeView> _treeNodes;
		public ObservableCollection<NodeView> TreeViewList
		{
			get
			{
				return _treeNodes;
			}
			set { 
				_treeNodes = value; 
			}
		}

		public Node TreeResult
		{
			set
			{
				if (value != null)
				{
					_treeNodes = new ObservableCollection<NodeView>();
					_treeNodes.Add(value.ToTreeViewNode());
					OnPropertyChanged("TreeViewList");
				}
			}
		}



		private RelayCommand _chooseFile;
		public RelayCommand ChooseFile
		{
			get
			{
				return _chooseFile ??= new RelayCommand(obj =>
				{
					using var dialog = new FolderBrowserDialog();

					if( dialog.ShowDialog() == DialogResult.OK)
					{
						FilePath = dialog.SelectedPath;
						IsFileChosen = true;
					}
				});
			}
		}		

		private RelayCommand _startSearch;
		public RelayCommand StartSearch
		{
			get
			{
				return _startSearch ??= new RelayCommand(obj =>
				{
					_scanner = new LocalStorageScanner();
					
					Task.Run(() =>
					{
						IsSearchEnabled = true;
						_scanner.Start(FilePath);

						while (!_scanner.IsFinish()){}

						_scanner.Stop();
						var result = _scanner.GetResult();

						IsSearchEnabled = false;

						TreeResult = result;
					});
				});
			}
		}

		private RelayCommand _stopScan;
		public RelayCommand StopScan
		{
			get
			{
				return _stopScan ??= new RelayCommand(obj =>
				{
					_scanner?.Stop();
				});
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
