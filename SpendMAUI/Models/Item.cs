using System.Collections.ObjectModel;

namespace SpendMAUI.Models
{
    public class Item
    {
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
        public string Money { get; set; }

    }
}