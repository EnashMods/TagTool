﻿using TagTool.Serialization;

namespace TagTool.Common
{
    [TagStructure(Size = 0x10)]
    public class ResourceGen2
    {
        public ResourceTypeGen2 Type;
        public sbyte AlignmentBit;
        public short FieldOffset;
        public short PrimaryLocator;
        public short SecondaryLocator;
        public int ResoureDataSize;
        public int ResourceDataOffset;
    }
}