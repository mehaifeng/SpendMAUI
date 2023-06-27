using System.Collections.ObjectModel;

namespace SpendMAUI.Models
{
    public class Item
    {
        /// <summary>
        /// 是否为收入
        /// </summary>
        public bool IsIncome { get; set; }
        /// <summary>
        /// 收支名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 收支描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 收支金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 收支发生的时间
        /// </summary>
        public string Time { get; set; }

    }
}