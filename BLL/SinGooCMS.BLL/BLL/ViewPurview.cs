using SinGooCMS.Entity;
using System;

namespace SinGooCMS.BLL
{
	public class ViewPurview
	{
		public int NodeID
		{
			get;
			set;
		}

		public bool NeedLogin
		{
			get;
			set;
		}

		public string GroupPurview
		{
			get;
			set;
		}

		public string LevelPurview
		{
			get;
			set;
		}

		public ViewPurview GetAccessPurview(NodeInfo currNode, ref ViewPurview vp)
		{
			if (currNode.NodeSetting.NeedLogin)
			{
				vp.NodeID = currNode.AutoID;
				vp.NeedLogin = true;
				vp.GroupPurview = currNode.NodeSetting.EnableViewUGroups;
				vp.LevelPurview = currNode.NodeSetting.EnableViewULevel;
			}
			ViewPurview result;
			if (currNode.ParentID > 0)
			{
				result = this.GetAccessPurview(Node.GetCacheNodeById(currNode.ParentID), ref vp);
			}
			else
			{
				result = vp;
			}
			return result;
		}
	}
}
