using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using XLCCoin.Application.NodeCommands.Queries;

namespace XLCCoin.NodeApp
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public MainWindowVM()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }


        public ObservableCollection<LogData> Log { get; set; } = new ObservableCollection<LogData>();
        public ObservableCollection<NodeVM> ConnectedNodes { get; set; } = new ObservableCollection<NodeVM>();

        public void AddLog(string log)
        {
            Log.Add(new LogData { Data = log, LogDate = DateTime.Now });

            OnPropertyChanged(new PropertyChangedEventArgs("LogX"));
        }

        public void AddConnectedNode(NodeVM node)
        {
            ConnectedNodes.Add(node);

            OnPropertyChanged(new PropertyChangedEventArgs("ConnectedNode"));
        }
    }

    public class LogData
    {
        public string Data { get; set; }
        public DateTime LogDate { get; set; }
    }
}
