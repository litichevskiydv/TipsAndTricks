namespace TipsAndTricksLibrary.Cryptography.Rsa
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    public class OpensslFormatParser : IRsaKeysParser
    {
        private static int DetermineBytesCount(BinaryReader reader)
        {
            int count;
            byte bt;

            bt = reader.ReadByte();
            if (bt != 0x02)
                return 0;

            bt = reader.ReadByte();
            switch (bt)
            {
                case 0x81:
                    count = reader.ReadByte();
                    break;
                case 0x82:
                    var highbyte = reader.ReadByte();
                    var lowbyte = reader.ReadByte();
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                    count = BitConverter.ToInt32(modint, 0);
                    break;
                default:
                    count = bt;
                    break;
            }

            while (reader.ReadByte() == 0x00)
                count -= 1;
            reader.BaseStream.Seek(-1, SeekOrigin.Current);

            return count;
        }

        public RSAParameters ParsePrivateKey(string key)
        {
            return ParsePrivateKey(Convert.FromBase64String(key));
        }

        public RSAParameters ParsePrivateKey(byte[] key)
        {
            var parameters = new RSAParameters();

            using (var reader = new BinaryReader(new MemoryStream(key)))
            {
                ushort twoBytes;
                twoBytes = reader.ReadUInt16();
                switch (twoBytes)
                {
                    case 0x8130:
                        reader.ReadByte();
                        break;
                    case 0x8230:
                        reader.ReadInt16();
                        break;
                    default:
                        throw new ArgumentException("Given key is invalid");
                }

                if (reader.ReadUInt16() != 0x0102)
                    throw new ArgumentException("Unexpected key version");

                if (reader.ReadByte() != 0x00)
                    throw new ArgumentException("Given key is invalid");

                parameters.Modulus = reader.ReadBytes(DetermineBytesCount(reader));
                parameters.Exponent = reader.ReadBytes(DetermineBytesCount(reader));
                parameters.D = reader.ReadBytes(DetermineBytesCount(reader));
                parameters.P = reader.ReadBytes(DetermineBytesCount(reader));
                parameters.Q = reader.ReadBytes(DetermineBytesCount(reader));
                parameters.DP = reader.ReadBytes(DetermineBytesCount(reader));
                parameters.DQ = reader.ReadBytes(DetermineBytesCount(reader));
                parameters.InverseQ = reader.ReadBytes(DetermineBytesCount(reader));
            }

            return parameters;
        }
    }
}