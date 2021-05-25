﻿using System;
using System.Collections.Generic;
using System.Text;
using Lekarnik.Models;
using Xamarin.Forms;

namespace Lekarnik
{
    class TodoItemPageCS : ContentPage
    {
        public TodoItemPageCS()
        {
            Title = "Zápisník";

            var nameEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, "Name");

            var notesEntry = new Entry();
            notesEntry.SetBinding(Entry.TextProperty, "Poznámky");

            var doneSwitch = new Switch();
            doneSwitch.SetBinding(Switch.IsToggledProperty, "Done");

            var saveButton = new Button { Text = "Uložiť" };
            saveButton.Clicked += async (sender, e) =>
            {
                var todoItem = (TodoItem)BindingContext;
                TodoItemDatabase database = await TodoItemDatabase.Instance;
                await database.SaveItemAsync(todoItem);
                await Navigation.PopAsync();
            };

            var deleteButton = new Button { Text = "Vymazať" };
            deleteButton.Clicked += async (sender, e) =>
            {
                var todoItem = (TodoItem)BindingContext;
                TodoItemDatabase database = await TodoItemDatabase.Instance;
                await database.DeleteItemAsync(todoItem);
                await Navigation.PopAsync();
            };

            var cancelButton = new Button { Text = "Ukončiť" };
            cancelButton.Clicked += async (sender, e) =>
            {
                await Navigation.PopAsync();
            };

            Content = new StackLayout
            {
                Margin = new Thickness(20),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children =
                {
                    new Label { Text = "Name" },
                    nameEntry,
                    new Label { Text = "Notes" },
                    notesEntry,
                    new Label { Text = "Done" },
                    doneSwitch,
                    saveButton,
                    deleteButton,
                    cancelButton
                }
            };
        }
    }
}

