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
		
        public void Load(MyDataRow row)
        {
            Id = DataParser.Parse<int>(row, "Id", -1);
			StartTime = DataParser.Parse<int>(row, "StartTime", -1);
			ClipName = DataParser.Parse<string>(row, "ClipName", string.Empty);
			Layer = DataParser.Parse<int>(row, "Layer", -1);
			Speed = DataParser.Parse<float>(row, "Speed", 0f);
			Weight = DataParser.Parse<float>(row, "Weight", 0f);
			WrapMode = DataParser.Parse<string>(row, "WrapMode", string.Empty);
			BlendMode = DataParser.Parse<string>(row, "BlendMode", string.Empty);
			CrossTime = DataParser.Parse<float>(row, "CrossTime", 0f);
			
        }
        public int GetId()
        {
            return Id;
        }
    }
    public class Tab_AnimationDataData : DataInstance<Tab_AnimationData> { }
    
    public class Tab_BufferData : IDataUnit
    {
        public int Id;
		public int LogicId;
		public List<int> EffectIdList;
		public List<int> AnimationIdList;
		public List<int> SoundIdList;
		public List<string> ParamList;
		
        public void Load(MyDataRow row)
        {
            Id = DataParser.Parse<int>(row, "Id", -1);
			LogicId = DataParser.Parse<int>(row, "LogicId", -1);
			EffectIdList = DataParser.ParseList<int>(row, "EffectId", -1);
			AnimationIdList = DataParser.ParseList<int>(row, "AnimationId", -1);
			SoundIdList = DataParser.ParseList<int>(row, "SoundId", -1);
			ParamList = DataParser.ParseList<string>(row, "Param", string.Empty);
			
        }
        public int GetId()
        {
            return Id;
        }
    }
    public class Tab_BufferDataData : DataInstance<Tab_BufferData> { }
    
    public class Tab_EffectData : IDataUnit
    {
        public int Id;
		public int StartTime;
		public int DeleteTime;
		public string Resource;
		public string Bone;
		public int IsAttach;
		public string Position;
		public string Rotation;
		public string Scale;
		public int IsBullet;
		
        public void Load(MyDataRow row)
        {
            Id = DataParser.Parse<int>(row, "Id", -1);
			StartTime = DataParser.Parse<int>(row, "StartTime", -1);
			DeleteTime = DataParser.Parse<int>(row, "DeleteTime", -1);
			Resource = DataParser.Parse<string>(row, "Resource", string.Empty);
			Bone = DataParser.Parse<string>(row, "Bone", string.Empty);
			IsAttach = DataParser.Parse<int>(row, "IsAttach", -1);
			Position = DataParser.Parse<string>(row, "Position", string.Empty);
			Rotation = DataParser.Parse<string>(row, "Rotation", string.Empty);
			Scale = DataParser.Parse<string>(row, "Scale", string.Empty);
			IsBullet = DataParser.Parse<int>(row, "IsBullet", -1);
			
        }
        public int GetId()
        {
            return Id;
        }
    }
    public class Tab_EffectDataData : DataInstance<Tab_EffectData> { }
    
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
			StartTime = DataParser.Parse<int>(row, "StartTime", -1);
			ParamList = DataParser.ParseList<string>(row, "Param", string.Empty);
			
        }
        public int GetId()
        {
            return Id;
        }
    }
    public class Tab_RuleDataData : DataInstance<Tab_RuleData> { }
    
    public class Tab_SkillData : IDataUnit
    {
        public int Id;
		public List<int> RuleIdList;
		public List<int> EffectIdList;
		public List<int> SoundIdList;
		public List<int> AnimationIdList;
		
        public void Load(MyDataRow row)
        {
            Id = DataParser.Parse<int>(row, "Id", -1);
			RuleIdList = DataParser.ParseList<int>(row, "RuleId", -1);
			EffectIdList = DataParser.ParseList<int>(row, "EffectId", -1);
			SoundIdList = DataParser.ParseList<int>(row, "SoundId", -1);
			AnimationIdList = DataParser.ParseList<int>(row, "AnimationId", -1);
			
        }
        public int GetId()
        {
            return Id;
        }
    }
    public class Tab_SkillDataData : DataInstance<Tab_SkillData> { }
    
    public class Tab_SoundData : IDataUnit
    {
        public int Id;
		public int StartTime;
		public string Resource;
		
        public void Load(MyDataRow row)
        {
            Id = DataParser.Parse<int>(row, "Id", -1);
			StartTime = DataParser.Parse<int>(row, "StartTime", -1);
			Resource = DataParser.Parse<string>(row, "Resource", string.Empty);
			
        }
        public int GetId()
        {
            return Id;
        }
    }
    public class Tab_SoundDataData : DataInstance<Tab_SoundData> { }
    
    public partial class DataProvider
    {
        private void LoadAllData()
        {
            LoadData(Tab_AnimationDataData.Instance, "Skill/Tab_AnimationData.txt");
			LoadData(Tab_BufferDataData.Instance, "Skill/Tab_BufferData.txt");
			LoadData(Tab_EffectDataData.Instance, "Skill/Tab_EffectData.txt");
			LoadData(Tab_RuleDataData.Instance, "Skill/Tab_RuleData.txt");
			LoadData(Tab_SkillDataData.Instance, "Skill/Tab_SkillData.txt");
			LoadData(Tab_SoundDataData.Instance, "Skill/Tab_SoundData.txt");
			
        }
    }
}
