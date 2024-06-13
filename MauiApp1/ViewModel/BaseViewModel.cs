﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool isBusy;
        string title;

        public bool IsBusy
        {
            get => isBusy;
            set {
                if (isBusy == value) {
                    return;
                }
                isBusy = value;
                OnPropertyChanged();
            }
        }
        
        public string Title
        {
            get => title;
            set {
                title = value;
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        public bool IsNotBusy => !isBusy;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
