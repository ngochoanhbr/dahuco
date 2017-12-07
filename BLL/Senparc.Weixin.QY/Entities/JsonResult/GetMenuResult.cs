﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.QY.Entities.Menu;

namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// GetMenu返回的Json结果
    /// </summary>
    public class GetMenuResult
    {
        public ButtonGroup menu { get; set; }

        public GetMenuResult()
        {
            menu = new ButtonGroup();
        }
    }
}
