﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Lekarnik.Models;


namespace Lekarnik.Controls
{
    class HodnotaSearchHandler : SearchHandler
    {

        public IList<Hodnota> Eco { get; set; }
        public Type SelectedItemNavigationTarget2 { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = Eco
                    .Where(hodnota => hodnota.NameOfHodnota.ToLower().Contains(newValue.ToLower()))
                    .ToList<Hodnota>();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            await Task.Delay(1000);

            ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;
            await Shell.Current.GoToAsync($"{GetNavigationTarget()}?nameOfHodnota={((Hodnota)item).NameOfHodnota}");
        }

        string GetNavigationTarget()
        {
            return (Shell.Current as AppShell).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget2)).Key;
        }

    }
}