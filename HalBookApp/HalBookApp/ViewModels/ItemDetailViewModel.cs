using HalBookApp.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HalBookApp.ViewModels
{
    //Show the book - ReadFile
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        public String ReadText(String fileName)
        {
            return File.ReadAllText(fileName);
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                if (item.Id == "1")
                {
                    item.Description = ReadText(@"C:\Users\hbradt\Downloads\HalBook.txt");
                    item.Text = "Book";
                }
                if (item.Id == "2")
                {
                    item.Text = "Downloading";
                    item.Description = "Downloading";
                }
  
                Text = item.Text; 
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
