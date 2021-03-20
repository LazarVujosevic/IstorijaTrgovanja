using IstorijaTrgovanjaLibrary;
using IstorijaTrgovanjaLibrary.ApiResultsData;
using IstorijaTrgovanjaLibrary.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace IstorijaTrgovanja
{
    public class TraziViewModel : INotifyPropertyChanged
    {
        public TraziCommand TraziCommand { get; private set; }

        public SearchCancelCommand SearchCancelCommand { get; private set; }

        public int MyProperty { get; set; }

        public MainWindow MainWindow { get; set; }

        public Api api { get; set; }

        public string Sifra { get; set; }

        private bool _isEnabled;
        public bool IsEnabled {
            get
            {
                return this._isEnabled;
            }

            set
            {
                this._isEnabled = value;
                if (this._isEnabled)
                {
                    this.IsContentForCancelSearchVisible = Visibility.Hidden;
                }
                else
                {
                    this.IsContentForCancelSearchVisible = Visibility.Visible;
                }

                NotifyPropertyChanged();
            } 
        }

        private Visibility _isContentForCancelSearchVisible;
        public Visibility IsContentForCancelSearchVisible
        {
            get
            {
                return this._isContentForCancelSearchVisible;
            }

            set
            {
                this._isContentForCancelSearchVisible = value;
                NotifyPropertyChanged();
            }
        }

        private List<ResultData> _resultsData;

        public List<ResultData> ResultsData
        {
            get
            {
                return _resultsData;
            }
            set
            {
                _resultsData = value;
                NotifyPropertyChanged();
            }
        }

        public TraziViewModel(MainWindow mainWindow)
        {
            TraziCommand = new TraziCommand(OnTraziCommand);
            SearchCancelCommand = new SearchCancelCommand(OnSearchCancelCommand);
            this.ResultsData = new List<ResultData>();
            this.MainWindow = mainWindow;
            this.IsEnabled = true;
            api = new Api();
        }

        public async void OnTraziCommand()
        {
            this.ResultsData.Clear();

            this.IsEnabled = false;
            var listOfDataStringsCsv = await api.CallApi(this.Sifra);
            if (listOfDataStringsCsv != null && listOfDataStringsCsv.Count > 0)
            {
                foreach (var dataRow in listOfDataStringsCsv)
                {
                    if (!string.IsNullOrEmpty(dataRow))
                    {
                        var dataArray = dataRow.Split(',');
                        this.ResultsData.Add(new ResultData
                        {
                            CreatedTime = DateTime.Parse(dataArray[0]),
                            Open = double.Parse(dataArray[1]),
                            High = double.Parse(dataArray[2]),
                            Low = double.Parse(dataArray[3]),
                            Close = double.Parse(dataArray[4]),
                            Volume = int.Parse(dataArray[5]),
                            MovingAverage = this.GetSumOfCloses(DateTime.Parse(dataArray[0]), double.Parse(dataArray[4])) / 10
                        });
                    }
                }
                this.IsEnabled = true;
            }

            MainWindow.Refresh();
            this.IsEnabled = true;
        }

        private void OnSearchCancelCommand()
        {
            this.IsEnabled = true;
        }

        private double? GetSumOfCloses(DateTime date, double close)
        {
            double? sum = 0;

            for (int i = 0; i <= 9; i++)
            {
                if (i == 0)
                {
                    sum += close;
                }
                else
                {
                    if (i > this.ResultsData.Count())
                    {
                        break;
                    }
                    var value = this.ResultsData.Where(x => x.CreatedTime == date.AddDays(-i)).FirstOrDefault();
                    if (value != null)
                    {
                        sum += value.Close;
                    }
                }
            }

            return sum;
        }

        public event PropertyChangedEventHandler PropertyChanged;
                
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
