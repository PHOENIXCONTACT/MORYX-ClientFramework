using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using C4I;
using Caliburn.Micro;

namespace Marvin.Controls.Demo.ViewModels
{
    public class ProgressViewModel : Screen
    {
        private int _firstValue = 120;
        private int _secondValue = 200;
        private int _thirdValue = 320;

        private string _startButtonContent = "Start";

        private Thread _t;
        private bool _run;
        private int _defaultCurrentValue;

        public override string DisplayName => "Progress";

        public int DefaultCurrentValue
        {
            get { return _defaultCurrentValue; }
            set
            {
                if (_defaultCurrentValue != value)
                {
                    _defaultCurrentValue = value;
                    NotifyOfPropertyChange();
                }
            }
        }


        public int MaxProgress => 1000;

        public int FirstValue
        {
            get { return _firstValue; }
            set
            {
                if (_firstValue != value)
                {
                    _firstValue = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public int SecondValue
        {
            get { return _secondValue; }
            set
            {
                if (_secondValue != value)
                {
                    _secondValue = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public int ThirdValue
        {
            get { return _thirdValue; }
            set
            {
                if (_thirdValue != value)
                {
                    _thirdValue = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public string StartButtonContent
        {
            get { return _startButtonContent; }
            set
            {
                if (_startButtonContent != value)
                {
                    _startButtonContent = value;
                    NotifyOfPropertyChange();
                }
            }
        }
        
        public ICommand IncreaseValueCommand { get; set; }

        public ICommand StartProgressCommand { get; set; }

        public ICommand IncreaseFirstCommand { get; set; }

        public ICommand IncreaseSecondCommand { get; set; }

        public ICommand IncreaseThirdCommand { get; set; }

        public ICommand DecreaseThirdCommand { get; set; }

        public ProgressViewModel()
        {
            IncreaseValueCommand = new RelayCommand(IncreaseValue);
            StartProgressCommand = new RelayCommand(StartProgress);
            IncreaseFirstCommand = new RelayCommand(IncreaseFirst);
            IncreaseSecondCommand = new RelayCommand(IncreaseSecond);
            IncreaseThirdCommand = new RelayCommand(IncreaseThird);
            DecreaseThirdCommand = new RelayCommand(DecreaseThird);
        }

        public void StartProgress(object parameters)
        {
            if (_t == null)
            {
                FirstValue = 0;
                SecondValue = 10;
                ThirdValue = 0;
                ThirdValue = 30;

                _run = true;
                _t = new Thread(o =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        StartButtonContent = "Stop";
                    });

                    while (_run)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (FirstValue + ThirdValue < MaxProgress)
                            {
                                FirstValue += 10;
                            }
                            else
                            {
                                FirstValue += 10;
                                ThirdValue -= 10;
                            }

                            if (ThirdValue <= 0)
                            {
                                _run = false;
                                _t = null;
                            }
                        });
                        Thread.Sleep(100);
                    }

                    Execute.OnUIThread(() => StartButtonContent = "Start");
                });

                _t.Start();
            }
            else
            {
                _run = false;
                try
                {
                    _t.Join(new TimeSpan(0, 0, 0, 0, 100));
                }
                catch
                {
                    // ignored
                }
                finally
                {
                    StartButtonContent = "Start";
                    _t = null;
                }
            }
        }

        public void IncreaseValue(object parameters)
        {
            DefaultCurrentValue += 25;
        }

        public void IncreaseFirst(object parameters)
        {
            FirstValue += 10;
        }

        public void IncreaseSecond(object parameters)
        {
            SecondValue += 10;
        }

        public void IncreaseThird(object parameters)
        {
            ThirdValue += 10;
        }

        public void DecreaseThird(object parameters)
        {
            ThirdValue -= 10;
        }
    }
}
