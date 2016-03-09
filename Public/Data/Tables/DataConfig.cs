///
/// This file is generated by machine, You shouldn't edit it manual.
///
using System;
using System.Collections.Generic;

namespace DataTableSpace
{
    
    public class Tab_AnimationData : IDataUnit
    {
        public int Id;
		public int StartTime;
		public string ClipName;
		public int Layer;
		public float Speed;
		public float Weight;
		public string WrapMode;
		public string BlendMode;
		public float CrossTime;
		public bool UseOnTarget;
		
        public void Load(MyDataRow row)
        {
            Id = DataParser.Parse<int>(row, "Id", -1);
			StartTime = DataParser.Parse<int>(row, "StartTime", 0);
			ClipName = DataParser.Parse<string>(row, "ClipName", string.Empty);
			Layer = DataParser.Parse<int>(row, "Layer", 0);
			Speed = DataParser.Parse<float>(row, "Speed", 1);
			Weight = DataParser.Parse<float>(row, "Weight", 1);
			WrapMode = DataParser.Parse<string>(row, "WrapMode", "Default");
			BlendMode = DataParser.Parse<string>(row, "BlendMode", "Blend");
			CrossTime = DataParser.Parse<float>(row, "CrossTime", 0.2f);
			UseOnTarget = DataParser.Parse<bool>(row, "UseOnTarget", true);
			
        }
        public int GetId()
        {
            return Id;
        }
    }
    public class Tab_AnimationDataProvider : DataInstance<Tab_AnimationData> { }
    
    public class Tab_BufferData : IDataUnit
    {
        public int Id;
		public int LogicId;
		public int Delay;
		public List<int> EffectIdList;
		public List<int> AnimationIdList;
		public List<int> SoundIdList;
		public List<int> MoveIdList;
		public List<string> ParamList;
		
        public void Load(MyDataRow row)
        {
            Id = DataParser.Parse<int>(row, "Id", -1);
			LogicId = DataParser.Parse<int>(row, "LogicId", -1);
			Delay = DataParser.Parse<int>(row, "Delay", 0);
			EffectIdList = DataParser.ParseList<int>(row, "EffectId", -1);
			AnimationIdList = DataParser.ParseList<int>(row, "AnimationId", -1);
			SoundIdList = DataParser.ParseList<int>(row, "SoundId", -1);
			MoveIdList = DataParser.ParseList<int>(row, "MoveId", -1);
			ParamList = DataParser.ParseList<string>(row, "Param", string.Empty);
			
        }
        public int GetId()
        {
            return Id;
        }
    }
    public class Tab_BufferDataProvider : DataInstance<Tab_BufferData> { }
    
    public class Tab_EffectData : IDataUnit
    {
        public int Id;
		public int StartTime;
		public int DeleteTime;
		public string Resource;
		public string Bone;
		public bool IsAttach;
		public string Position;
		public string Rotation;
		public string Scale;
		public bool UseOnTarget;
		public bool IsBullet;
		public int BulletTime;
		
        public void Load(MyDataRow row)
        {
            Id = DataParser.Parse<int>(row, "Id", -1);
			StartTime = DataParser.Parse<int>(row, "StartTime", 0);
			DeleteTime = DataParser.Parse<int>(row, "DeleteTime", 0);
			Resource = DataParser.Parse<string>(row, "Resource", string.Empty);
			Bone = DataParser.Parse<string>(row, "Bone", string.Empty);
			IsAttach = DataParser.Parse<bool>(row, "IsAttach", true);
			Position = DataParser.Parse<string>(row, "Position", "0,0,0");
			Rotation = DataParser.Parse<string>(row, "Rotation", "0,0,0");
			Scale = DataParser.Parse<string>(row, "Scale", "1,1,1");
			UseOnTarget = DataParser.Parse<bool>(row, "UseOnTarget", true);
			IsBullet = DataParser.Parse<bool>(row, "IsBullet", false);
			BulletTime = DataParser.Parse<int>(row, "BulletTime", 0);
			
        }
        public int GetId()
        {
            return Id;
        }
    }
    public class Tab_EffectDataProvider : DataInstance<Tab_EffectData> { }
    
    public class Tab_MoveData : IDataUnit
    {
        public int Id;
		public int StartTime;
		public bool IsLockTarget;
		public float Distance;
		public int MoveTime;
		public int CurveType;
		public float Direction;
		public bool IsRelativeSender;
		public bool UseOnTarget;
		
        public void Load(MyDataRow row)
        {
            Id = DataParser.Parse<int>(row, "Id", -1);
			StartTime = DataParser.Parse<int>(row, "StartTime", 0);
			IsLockTarget = DataParser.Parse<bool>(row, "IsLockTarget", true);
			Distance = DataParser.Parse<float>(row, "Distance", 0);
			MoveTime = DataParser.Parse<int>(row, "MoveTime", 0);
			CurveType = DataParser.Parse<int>(row, "CurveType", 0);
			Direction = DataParser.Parse<float>(row, "Direction", 0);
			IsRelativeSender = DataParser.Parse<bool>(row, "IsRelativeSender", true);
			UseOnTarget = DataParser.Parse<bool>(row, "UseOnTarget", true);
			
        }
        public int GetId()
        {
            return Id;
        }
    }
    public class Tab_MoveDataProvider : DataInstance<Tab_MoveData> { }
    
    public class Tab_RuleData : IDataUnit
    {
        public int Id;
		public int LogicId;
		public List<int> BufferIdList;
		public int StartTime;
		public List<string> ParamList;
		
        public void Load(MyDataRow row)
        {
            Id = DataParser.Parse<int>(row, "Id", -1);
			LogicId = DataParser.Parse<int>(row, "LogicId", -1);
			BufferIdList = DataParser.ParseList<int>(row, "BufferId", -1);
			StartTime = DataParser.Parse<int>(row, "StartTime", 0);
			ParamList = DataParser.ParseList<string>(row, "Param", string.Empty);
			
        }
        public int GetId()
        {
            return Id;
        }
    }
    public class Tab_RuleDataProvider : DataInstance<Tab_RuleData> { }
    
    public class Tab_SkillData : IDataUnit
    {
        public int Id;
		public float CheckRange;
		public List<int> RuleIdList;
		public List<int> EffectIdList;
		public List<int> SoundIdList;
		public List<int> AnimationIdList;
		public List<int> MoveIdList;
		
        public void Load(MyDataRow row)
        {
            Id = DataParser.Parse<int>(row, "Id", -1);
			CheckRange = DataParser.Parse<float>(row, "CheckRange", -1);
			RuleIdList = DataParser.ParseList<int>(row, "RuleId", -1);
			EffectIdList = DataParser.ParseList<int>(row, "EffectId", -1);
			SoundIdList = DataParser.ParseList<int>(row, "SoundId", -1);
			AnimationIdList = DataParser.ParseList<int>(row, "AnimationId", -1);
			MoveIdList = DataParser.ParseList<int>(row, "MoveId", -1);
			
        }
        public int GetId()
        {
            return Id;
        }
    }
    public class Tab_SkillDataProvider : DataInstance<Tab_SkillData> { }
    
    public class Tab_SoundData : IDataUnit
    {
        public int Id;
		public int StartTime;
		public string Resource;
		
        public void Load(MyDataRow row)
        {
            Id = DataParser.Parse<int>(row, "Id", -1);
			StartTime = DataParser.Parse<int>(row, "StartTime", 0);
			Resource = DataParser.Parse<string>(row, "Resource", string.Empty);
			
        }
        public int GetId()
        {
            return Id;
        }
    }
    public class Tab_SoundDataProvider : DataInstance<Tab_SoundData> { }
    
    public partial class DataProvider
    {
        private void LoadAllData()
        {
            LoadData(Tab_AnimationDataProvider.Instance, "Skill/Tab_AnimationData.txt");
			LoadData(Tab_BufferDataProvider.Instance, "Skill/Tab_BufferData.txt");
			LoadData(Tab_EffectDataProvider.Instance, "Skill/Tab_EffectData.txt");
			LoadData(Tab_MoveDataProvider.Instance, "Skill/Tab_MoveData.txt");
			LoadData(Tab_RuleDataProvider.Instance, "Skill/Tab_RuleData.txt");
			LoadData(Tab_SkillDataProvider.Instance, "Skill/Tab_SkillData.txt");
			LoadData(Tab_SoundDataProvider.Instance, "Skill/Tab_SoundData.txt");
			
        }
    }
}
