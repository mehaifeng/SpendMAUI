using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpendMAUI.Models;
using SpendMAUI.Views.Templates;
using Newtonsoft.Json;
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
        #region 构造函数
        public NoteSpendPageViewModel()
        {
            RAndSItems = new ObservableCollection<Item>();
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
            SelectDetailTypeItem = DetailType.First();
            if(RAndSItems.Count > 0)
            {
                ExpenseMoney = RAndSItems.Where(x => x.IsIncome == true).Sum(x => x.Money).ToString();
                InComeMoney = RAndSItems.Where(x => x.IsIncome == false).Sum(x => x.Money).ToString();
            }
            else
            {
                ExpenseMoney = "0";
                InComeMoney = "0";
            }
            if (File.Exists(path))
            {
                string jsonStr = File.ReadAllText(path);
                RAndSItems = JsonConvert.DeserializeObject<ObservableCollection<Item>>(jsonStr);
            }
            DateToday = DateOnly.FromDateTime(DateTime.Now);
        }
        #endregion

        #region 属性
        public static string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), $"{DateOnly.FromDateTime(DateTime.Now)}.json");
        public PopupNewItem popupNewItem;
        [ObservableProperty]
        private DateOnly dateToday;
        /// <summary>
        /// 收支项大集合
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<Item> rAndSItems;
        /// <summary>
        /// 支出
        /// </summary>
        [ObservableProperty]
        private string expenseMoney;
        /// <summary>
        /// 收入
        /// </summary>
        [ObservableProperty]
        private string inComeMoney;
        /// <summary>
        /// 待添加的收支项(popup弹窗)
        /// </summary>
        [ObservableProperty]
        private Item readyItem = new();
        /// <summary>
        /// 收支的具体类型
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<string> detailType;
        /// <summary>
        /// 选择的收支的具体类型
        /// </summary>
        [ObservableProperty]
        private string selectDetailTypeItem;
        /// <summary>
        /// 是否是支出
        /// </summary>
        [ObservableProperty]
        private bool isCheckExpense = true;
        /// <summary>
        /// 是否是收入
        /// </summary>
        [ObservableProperty]
        private bool isCheckInCome = false;
        #endregion

        #region 方法
        /// <summary>
        /// 添加收支项
        /// </summary>
        [RelayCommand]
        private void AddItem()
        {
            if(!string.IsNullOrEmpty(ReadyItem.Name) && decimal.IsPositive(ReadyItem.Money))
            {
                RAndSItems.Add(new Item
                {
                    IsIncome = IsCheckInCome,
                    Name = ReadyItem.Name,
                    Description = ReadyItem.Description,
                    Money = ReadyItem.Money,
                });
                popupNewItem.Close();
                InComeMoney = RAndSItems.Where(x => x.IsIncome == true).Sum(x => x.Money).ToString();
                ExpenseMoney = RAndSItems.Where(x => x.IsIncome == false).Sum(x => x.Money).ToString();
            }
            SaveFile();
        }
        private async void SaveFile()
        {
            string jsonStr = JsonConvert.SerializeObject(RAndSItems);
            await File.WriteAllTextAsync(path, jsonStr);
        }
        #endregion
    }
}
