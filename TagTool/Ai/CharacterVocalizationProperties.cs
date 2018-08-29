using TagTool.Serialization;

namespace TagTool.Ai
{
    [TagStructure(Size = 0xC)]
    public class CharacterVocalizationProperties
    {
        public float CharacterSkipFraction;
        public float LookCommentTime;
        public float LookLongCommentTime;
    }
}