using System;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;

namespace LIBUtil
{
    public enum BackgroundProcessCommand
    {
        PopulateDataGridView,
        Other
    }

    public class BackgroundProcess
    {
        private int _timerTimeout = 0;
        private int _timerInterval = 100;

        private static bool _isTaskCompleted;
        private static Timer _timer;
        private DateTime _timerStartTime;
        private BackgroundProcessCommand _processCommand;
        private static Desktop.Forms.ProgressBar_Form _progressBarForm = new Desktop.Forms.ProgressBar_Form();
        BackgroundWorker backgroundWorker = new BackgroundWorker();

        //DATAGRIDVIEW
        private DataGridView _datagridview = null;
        private object _data = null;

        public BackgroundProcess(int TimeoutInSeconds, DataGridView dgv, object data) : this()
        {
            _datagridview = dgv;
            _data = data;
            _processCommand = BackgroundProcessCommand.PopulateDataGridView;
            _timerTimeout = TimeoutInSeconds * 1000;
        }

        public BackgroundProcess()
        {
            _timer = new Timer();
            _timer.Tick += new System.EventHandler(timer_Tick);
            _timer.Interval = _timerInterval;

            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
        }

        public void run()
        {
            _isTaskCompleted = false;
            startTimer();
            backgroundWorker.RunWorkerAsync();
            Util.displayForm(null, _progressBarForm, false);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(500); //to allow enough time to show progress bar

            if(_processCommand == BackgroundProcessCommand.PopulateDataGridView)
                _datagridview.Invoke((Action)(() => _datagridview.DataSource = _data));

            _isTaskCompleted = true;
            _progressBarForm.Invoke((Action)(() => _progressBarForm.Close()));
        }

        private void startTimer()
        {
            _timer.Start();
            _timerStartTime = DateTime.Now;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (_isTaskCompleted)
                _timer.Stop();
            else if (_timerTimeout > 0 && (DateTime.Now - _timerStartTime).TotalMilliseconds > _timerTimeout)
            {
                _timer.Stop();
                _progressBarForm.Close();
                Util.displayMessageBoxError("Process exceed timeout limit. Please try again");
            }
        }
    }
}
