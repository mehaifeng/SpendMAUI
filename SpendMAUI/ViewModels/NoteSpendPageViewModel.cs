using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpendMAUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpendMAUI.Views.Popups;

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
            string todaypath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{DateTime.Now.Date:yyMMdd}.json");
            if (File.Exists(todaypath))
            {
                string jsonStr = File.ReadAllText(todaypath);
                RAndSItems = JsonConvert.DeserializeObject<ObservableCollection<Item>>(jsonStr);
                IsShowNonDataImage = RAndSItems.Count > 0 ? false : true;
            }
            if (RAndSItems.Count > 0)
            {
                CaculateSum();
            }
            SelectDate = DateTime.Now.Date;
        }
        #endregion

        #region 属性
        public PopupNewItem popupNewItem;
        /// <summary>
        /// 选择的日期
        /// </summary>
        [ObservableProperty]
        private DateTime selectDate;
        /// <summary>
        /// 收支项大集合
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<Item> rAndSItems;
        /// <summary>
        /// 支出
        /// </summary>
        [ObservableProperty]
        private string expenseMoney = "0";
        /// <summary>
        /// 收入
        /// </summary>
        [ObservableProperty]
        private string inComeMoney = "0";
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
        /// <summary>
        /// 收支产生的时间
        /// </summary>
        [ObservableProperty]
        private string time;
        /// <summary>
        /// 是否展示无数据的图片
        /// </summary>
        [ObservableProperty]
        private bool isShowNonDataImage;
        #endregion

        #region 方法
        /// <summary>
        /// 打开Popup
        /// </summary>
        /// <param name="e"></param>
        [RelayCommand]
        private void OpenPopupToAdd(ContentPage e)
        {
            ReadyItem = new Item();
            popupNewItem = new PopupNewItem();
            e.ShowPopup(popupNewItem);
        }
        /// <summary>
        /// 添加收支项
        /// </summary>
        [RelayCommand]
        private void AddItem()
        {
            if (!string.IsNullOrEmpty(ReadyItem.Name) && decimal.IsPositive(ReadyItem.Money))
            {
                popupNewItem.Close();
                RAndSItems.Add(new Item
                {
                    IsIncome = IsCheckInCome,
                    Name = ReadyItem.Name,
                    Description = ReadyItem.Description,
                    Money = ReadyItem.Money,
                    Time = DateTime.Now.ToString("HH:mm")
                });
                IsShowNonDataImage = false;
                CaculateSum();
            }
            SaveFile();
        }
        /// <summary>
        /// 删除收支项
        /// </summary>
        [RelayCommand]
        private void DeleteItem(Item o)
        {
            RAndSItems.Remove(o);
        }
        /// <summary>
        /// 选择日期操作
        /// </summary>
        [RelayCommand]
        private async void SelectedDate()
        {
            RAndSItems = new ObservableCollection<Item>();
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{SelectDate:yyMMdd}.json");
            if (File.Exists(path))
            {
                string jsonStr = await File.ReadAllTextAsync(path);
                RAndSItems = JsonConvert.DeserializeObject<ObservableCollection<Item>>(jsonStr);
                IsShowNonDataImage = RAndSItems.Count > 0 ? false : true;
                CaculateSum();
            }
            else
            {
                CaculateSum();
                IsShowNonDataImage = true;
            }
        }
        /// <summary>
        /// 计算支出
        /// </summary>
        public void CaculateSum()
        {
            InComeMoney = RAndSItems.Where(x => x.IsIncome == true).Sum(x => x.Money).ToString();
            ExpenseMoney = RAndSItems.Where(x => x.IsIncome == false).Sum(x => x.Money).ToString();
        }
        /// <summary>
        /// 记录保存到文件
        /// </summary>
        private async void SaveFile()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{SelectDate:yyMMdd}.json");
            string jsonStr = JsonConvert.SerializeObject(RAndSItems);
            await File.WriteAllTextAsync(path, jsonStr);
        }
        #endregion
    }
}
