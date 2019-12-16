using DevExpress.XtraGrid.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublicManager
{
    public class BuilderGridLocalizer : GridLocalizer
    {
        Dictionary<GridStringId, string> CusLocalizedKeyValue = null;
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="cusLocalizedKeyValue">需要转移的GridStringId，其对应的文字描述</param>
        public BuilderGridLocalizer(Dictionary<GridStringId, string> cusLocalizedKeyValue)
        {
            CusLocalizedKeyValue = cusLocalizedKeyValue;
        }
        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="id">GridStringId</param>
        /// <returns>string</returns>
        public override string GetLocalizedString(GridStringId id)
        {
            if (CusLocalizedKeyValue != null)
            {
                string _gridStringDisplay = string.Empty;
                foreach (KeyValuePair<GridStringId, string> gridLocalizer in CusLocalizedKeyValue)
                {
                    if (gridLocalizer.Key.Equals(id))
                    {
                        _gridStringDisplay = gridLocalizer.Value;
                        break;
                    }
                }
                return _gridStringDisplay;
            }
            return base.GetLocalizedString(id);
        }
    }
}