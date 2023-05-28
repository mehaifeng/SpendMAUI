using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpendMAUI.Models;
using SpendMAUI.Views.Templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendMAUI.ViewModels
{
    public partial class NoteSpendPageViewModel:ObservableObject
    {
        PopupNewItem popupNewItem;
        public NoteSpendPageViewModel()
        {
            RAndSItems = new ObservableCollection<Item>
            {
                new Item{ Name = "buy some food", Money = "100" },
                new Item{ Name = "buy some drink", Money = "200" },
                new Item{ Name = "buy some clothes", Money = "300" },
                new Item{ Name = "buy some shoes", Money = "400" },
                new Item{ Name = "buy some books", Money = "500" },
                new Item{ Name = "buy some pens", Money = "600" },
                new Item{ Name = "buy some pencils", Money = "700" },
                new Item{ Name = "buy some bags", Money = "800" },
                new Item{ Name = "buy some hats", Money = "900" },
                new Item{ Name = "buy some glasses", Money = "1000" },
                new Item{ Name = "buy some toys", Money = "1100" },
                new Item{ Name = "buy some games", Money = "1200" },
            };
            Type = new ObservableCollection<string>
            {
                "收入",
                "支出"
            };
            DetailType = new ObservableCollection<string>
            {
                "饮食",
                "服装",
                "住宿",
                "交通",
                "娱乐",
                "教育",
                "金融"
            };
            SelectTypeItem = Type.First();
            SelectDetailTypeItem = DetailType.First();
        }
        /// <summary>
        /// 收支项大集合
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<Item> rAndSItems;

        /// <summary>
        /// 待添加的收支项(popup弹窗)
        /// </summary>
        [ObservableProperty]
        private Item readyItem = new();

        /// <summary>
        /// 开支还是收入
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<string> type;
        /// <summary>
        /// 收支的具体类型
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<string> detailType;
        /// <summary>
        /// 选择的收支类型
        /// </summary>
        [ObservableProperty]
        private string selectTypeItem;
        /// <summary>
        /// 选择的收支的具体类型
        /// </summary>
        [ObservableProperty]
        private string selectDetailTypeItem;

        /// <summary>
        /// 点击【添加】待添加收支项
        /// </summary>
        /// <param name="e"></param>
        [RelayCommand]
        private void ReadyAddItem(ContentPage e)
        {
            popupNewItem = new();
            ReadyItem = new();
            e.ShowPopup(popupNewItem);
        }

        [RelayCommand]
        private void AddItem()
        {
            RAndSItems.Add(new Item
            {
                Name = ReadyItem.Name,
                Description = ReadyItem.Description,
                Money = ReadyItem.Money,
            });
            popupNewItem.Close();
        }

    }
}
