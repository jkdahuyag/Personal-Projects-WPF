﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalComputerSolutionsApp
{
	public class NavigationDrawerModel
	{
		private string _item;
		public string Item
		{
			get { return _item; }
			set { _item = value; }
		}

		private object _icon;
		public object Icon
		{
			get { return _icon; }
			set { _icon = value; }
		}
	}
}
