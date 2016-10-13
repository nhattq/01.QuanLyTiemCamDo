using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QLCamDo.Helpers
{
    public class EncryptHelper
    {
        const byte Nb = 4;

        byte[] DEFAULT_KEY;

        int m_Nr;
        int m_Nk;

        byte[] m_szIn;
        byte[] m_szOut;
        byte[][] m_state;
        byte[] m_RoundKey;
        byte[] m_szKey;
        byte[] m_szTemp;

        public static string EncryptMD5Hash(string input)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }

        public EncryptHelper()
        {
            //
            // TODO: Add constructor logic here
            //
            m_szIn = new byte[16];
            m_szOut = new byte[16];
            m_RoundKey = new byte[240];
            m_szKey = new byte[32];
            m_state = new byte[4][];
            for (int i = 0; i < 4; ++i)
            {
                m_state[i] = new byte[4];
            }

            Encoding enco = Encoding.UTF8;
            DEFAULT_KEY = enco.GetBytes("nhatlinhtst.com");
            ZeroMemory(m_szKey, 32);
            byte[] szKey = enco.GetBytes("nhatlinhtst.com");
            for (int i = 0; i < szKey.GetLength(0); ++i) m_szKey[i] = szKey[i];
        }

        byte xtime(byte x)
        {
            return (byte)((x << 1) ^ (((x >> 7) & 1) * 0x1B));
        }

        byte Multiply(byte x, byte y)
        {
            return (byte)(((y & 1) * x) ^ ((y >> 1 & 1) * xtime(x)) ^ ((y >> 2 & 1) * xtime(xtime(x))) ^ ((y >> 3 & 1) * xtime(xtime(xtime(x)))) ^ ((y >> 4 & 1) * xtime(xtime(xtime(xtime(x))))));
        }

        byte getSBoxValue(int num)
        {
            int[] sbox =   {
                //0     1     2     3     4     5     6     7     8     9     A     B     C     D     E     F
                0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76, //0
                0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0, //1
                0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15, //2
                0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75, //3
                0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84, //4
                0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf, //5
                0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8, //6
                0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2, //7
                0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73, //8
                0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb, //9
                0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79, //A
                0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08, //B
                0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a, //C
                0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e, //D
                0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf, //E
                0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16 }; //F
            return (byte)sbox[num];
        }

        byte getSBoxInvert(int num)
        {
            int[] rsbox =
        { 0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb
        , 0x7c, 0xe3, 0x39, 0x82, 0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb
        , 0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d, 0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e
        , 0x08, 0x2e, 0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25
        , 0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92
        , 0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15, 0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84
        , 0x90, 0xd8, 0xab, 0x00, 0x8c, 0xbc, 0xd3, 0x0a, 0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3, 0x45, 0x06
        , 0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02, 0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b
        , 0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea, 0x97, 0xf2, 0xcf, 0xce, 0xf0, 0xb4, 0xe6, 0x73
        , 0x96, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e
        , 0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7, 0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b
        , 0xfc, 0x56, 0x3e, 0x4b, 0xc6, 0xd2, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd, 0x5a, 0xf4
        , 0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31, 0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f
        , 0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d, 0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef
        , 0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0, 0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61
        , 0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0c, 0x7d };

            return (byte)rsbox[num];
        }

        void KeyExpansion()
        {
            int[] Rcon = {
            0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a,
            0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39,
            0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a,
            0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8,
            0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef,
            0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc,
            0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b,
            0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3,
            0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94,
            0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20,
            0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35,
            0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f,
            0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04,
            0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63,
            0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd,
            0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb  };

            int i, j;
            byte[] temp = { 0, 0, 0, 0 };
            byte k;

            // The first round key is the key itself.
            for (i = 0; i < m_Nk; i++)
            {
                m_RoundKey[i * 4] = m_szKey[i * 4];
                m_RoundKey[i * 4 + 1] = m_szKey[i * 4 + 1];
                m_RoundKey[i * 4 + 2] = m_szKey[i * 4 + 2];
                m_RoundKey[i * 4 + 3] = m_szKey[i * 4 + 3];
            }

            // All other round keys are found from the previous round keys.
            while (i < (Nb * (m_Nr + 1)))
            {
                for (j = 0; j < 4; j++)
                {
                    temp[j] = m_RoundKey[(i - 1) * 4 + j];
                }
                if (i % m_Nk == 0)
                {
                    // This function rotates the 4 bytes in a word to the left once.
                    // [a0,a1,a2,a3] becomes [a1,a2,a3,a0]

                    // Function RotWord()
                    {
                        k = temp[0];
                        temp[0] = temp[1];
                        temp[1] = temp[2];
                        temp[2] = temp[3];
                        temp[3] = k;
                    }

                    // SubWord() is a function that takes a four-byte input word and 
                    // applies the S-box to each of the four bytes to produce an output word.

                    // Function Subword()
                    {
                        temp[0] = getSBoxValue(temp[0]);
                        temp[1] = getSBoxValue(temp[1]);
                        temp[2] = getSBoxValue(temp[2]);
                        temp[3] = getSBoxValue(temp[3]);
                    }

                    temp[0] = (byte)(temp[0] ^ (byte)Rcon[i / m_Nk]);
                }
                else if (m_Nk > 6 && i % m_Nk == 4)
                {
                    // Function Subword()
                    {
                        temp[0] = getSBoxValue(temp[0]);
                        temp[1] = getSBoxValue(temp[1]);
                        temp[2] = getSBoxValue(temp[2]);
                        temp[3] = getSBoxValue(temp[3]);
                    }
                }
                m_RoundKey[i * 4 + 0] = (byte)((byte)m_RoundKey[(i - m_Nk) * 4 + 0] ^ (byte)temp[0]);
                m_RoundKey[i * 4 + 1] = (byte)((byte)m_RoundKey[(i - m_Nk) * 4 + 1] ^ (byte)temp[1]);
                m_RoundKey[i * 4 + 2] = (byte)((byte)m_RoundKey[(i - m_Nk) * 4 + 2] ^ (byte)temp[2]);
                m_RoundKey[i * 4 + 3] = (byte)((byte)m_RoundKey[(i - m_Nk) * 4 + 3] ^ (byte)temp[3]);
                i++;
            }
        }

        void AddRoundKey(int round)
        {
            int i, j;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    m_state[j][i] ^= m_RoundKey[round * Nb * 4 + i * Nb + j];
                }
            }
        }

        void SubBytes()
        {
            int i, j;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    m_state[i][j] = getSBoxValue(m_state[i][j]);
                }
            }
        }

        void InvSubBytes()
        {
            int i, j;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    m_state[i][j] = getSBoxInvert(m_state[i][j]);
                }
            }
        }

        void ShiftRows()
        {
            byte temp;

            // Rotate first row 1 columns to left	
            temp = m_state[1][0];
            m_state[1][0] = m_state[1][1];
            m_state[1][1] = m_state[1][2];
            m_state[1][2] = m_state[1][3];
            m_state[1][3] = temp;

            // Rotate second row 2 columns to left	
            temp = m_state[2][0];
            m_state[2][0] = m_state[2][2];
            m_state[2][2] = temp;

            temp = m_state[2][1];
            m_state[2][1] = m_state[2][3];
            m_state[2][3] = temp;

            // Rotate third row 3 columns to left
            temp = m_state[3][0];
            m_state[3][0] = m_state[3][3];
            m_state[3][3] = m_state[3][2];
            m_state[3][2] = m_state[3][1];
            m_state[3][1] = temp;
        }

        void InvShiftRows()
        {
            byte temp;
            // Rotate first row 1 columns to right	
            temp = m_state[1][3];
            m_state[1][3] = m_state[1][2];
            m_state[1][2] = m_state[1][1];
            m_state[1][1] = m_state[1][0];
            m_state[1][0] = temp;

            // Rotate second row 2 columns to right	
            temp = m_state[2][0];
            m_state[2][0] = m_state[2][2];
            m_state[2][2] = temp;

            temp = m_state[2][1];
            m_state[2][1] = m_state[2][3];
            m_state[2][3] = temp;

            // Rotate third row 3 columns to right
            temp = m_state[3][0];
            m_state[3][0] = m_state[3][1];
            m_state[3][1] = m_state[3][2];
            m_state[3][2] = m_state[3][3];
            m_state[3][3] = temp;
        }

        void MixColumns()
        {
            int i;
            byte Tmp, Tm, t;
            for (i = 0; i < 4; i++)
            {
                t = m_state[0][i];
                Tmp = (byte)(m_state[0][i] ^ m_state[1][i] ^ m_state[2][i] ^ m_state[3][i]);
                Tm = (byte)(m_state[0][i] ^ m_state[1][i]); Tm = xtime(Tm); m_state[0][i] ^= (byte)(Tm ^ Tmp);
                Tm = (byte)((byte)m_state[1][i] ^ (byte)m_state[2][i]); Tm = xtime(Tm); m_state[1][i] ^= (byte)(Tm ^ Tmp);
                Tm = (byte)((byte)m_state[2][i] ^ (byte)m_state[3][i]); Tm = xtime(Tm); m_state[2][i] ^= (byte)(Tm ^ Tmp);
                Tm = (byte)(m_state[3][i] ^ t); Tm = xtime(Tm); m_state[3][i] ^= (byte)(Tm ^ Tmp);
            }
        }
        void InvMixColumns()
        {
            int i;
            byte a, b, c, d;
            for (i = 0; i < 4; i++)
            {

                a = m_state[0][i];
                b = m_state[1][i];
                c = m_state[2][i];
                d = m_state[3][i];


                m_state[0][i] = (byte)(Multiply(a, 0x0e) ^ Multiply(b, 0x0b) ^ Multiply(c, 0x0d) ^ Multiply(d, 0x09));
                m_state[1][i] = (byte)(Multiply(a, 0x09) ^ Multiply(b, 0x0e) ^ Multiply(c, 0x0b) ^ Multiply(d, 0x0d));
                m_state[2][i] = (byte)(Multiply(a, 0x0d) ^ Multiply(b, 0x09) ^ Multiply(c, 0x0e) ^ Multiply(d, 0x0b));
                m_state[3][i] = (byte)(Multiply(a, 0x0b) ^ Multiply(b, 0x0d) ^ Multiply(c, 0x09) ^ Multiply(d, 0x0e));
            }

        }
        void Cipher()
        {
            int i, j, round = 0;
            //Copy the input PlainText to state array.
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    m_state[j][i] = m_szIn[i * 4 + j];
                }
            }
            // Add the First round key to the state before starting the rounds.
            AddRoundKey(0);

            // There will be Nr rounds.
            // The first Nr-1 rounds are identical.
            // These Nr-1 rounds are executed in the loop below.
            for (round = 1; round < m_Nr; round++)
            {
                SubBytes();
                ShiftRows();
                MixColumns();
                AddRoundKey(round);
            }

            // The last round is given below.
            // The MixColumns function is not here in the last round.
            SubBytes();
            ShiftRows();
            AddRoundKey(m_Nr);

            // The encryption process is over.
            // Copy the state array to output array.
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    m_szOut[i * 4 + j] = m_state[j][i];
                }
            }
        }

        void InvCipher()
        {
            int i, j, round = 0;

            //Copy the input CipherText to state array.
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    m_state[j][i] = m_szIn[i * 4 + j];
                }
            }

            // Add the First round key to the state before starting the rounds.
            AddRoundKey(m_Nr);

            // There will be Nr rounds.
            // The first Nr-1 rounds are identical.
            // These Nr-1 rounds are executed in the loop below.
            for (round = m_Nr - 1; round > 0; round--)
            {
                InvShiftRows();
                InvSubBytes();
                AddRoundKey(round);
                InvMixColumns();
            }

            // The last round is given below.
            // The MixColumns function is not here in the last round.
            InvShiftRows();
            InvSubBytes();
            AddRoundKey(0);

            // The decryption process is over.
            // Copy the state array to output array.
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    m_szOut[i * 4 + j] = m_state[j][i];
                }
            }
        }

        int GetLength(byte[] str)
        {
            int iSize = str.GetLength(0);
            int i = 0;
            while (i < iSize)
            {
                if (str[i] == 0) return i;
                ++i;
            }
            return iSize;
        }

        void ZeroMemory(byte[] p, int nSize)
        {
            for (int i = 0; i < nSize; ++i) p[i] = 0;
        }

        void XOR(bool nOut)
        {

            int klen = GetLength(m_szKey);
            int k = 0;
            if (nOut)
            {
                for (int i = 0; i < 16; ++i)
                {
                    m_szOut[i] = (byte)(m_szOut[i] ^ m_szKey[k]);
                    k = (++k < klen ? k : 0);
                }
            }
            else
            {
                for (int i = 0; i < 16; ++i)
                {
                    m_szIn[i] = (byte)(m_szIn[i] ^ m_szKey[k]);
                    k = (++k < klen ? k : 0);
                }

            }
        }

        public string Encrypt(string szClearText)
        {
            if (szClearText == null) return "";
            // Calculate Nk and Nr from the recieved value.
            m_Nr = 256;
            m_Nk = m_Nr / 32;
            m_Nr = m_Nk + 6;

            Encoding enco = Encoding.Unicode;
            byte[] szText = enco.GetBytes(szClearText);

            int iLen = szText.GetLength(0);
            if (iLen == 0)
            {
                iLen = 16;
                szText = new byte[16];
                memset(szText, 0, 16);
            }
            int i = 0;

            string szEncrypt = "";
            string st;
            while (i < iLen)
            {
                memset(m_szIn, 0, 16);
                memset(m_szOut, 0, 16);

                for (int j = 0; j < 16; ++j)
                {
                    if ((i + j) < iLen)
                        m_szIn[j] = szText[i + j]; //memcpy_s(m_szIn, 16, szClearText[i], 16);
                    else
                        m_szIn[j] = 0;
                }

                KeyExpansion();
                Cipher();

                XOR(true);

                for (int j = 0; j < Nb * 4; j++)
                {
                    st = System.Convert.ToString(m_szOut[j], 16).ToUpper();
                    if (m_szOut[j] < 16) st = "0" + st;
                    szEncrypt += st;
                }

                i += 16;
            }

            return szEncrypt;
        }

        public string Decrypt(string szCipherText)
        {
            if (szCipherText == null || szCipherText == "") return "";
            // Calculate Nk and Nr from the recieved value.
            m_Nr = 256;
            m_Nk = m_Nr / 32;
            m_Nr = m_Nk + 6;

            Encoding encoW = Encoding.Unicode;

            Encoding enco = Encoding.UTF8;
            byte[] szText = enco.GetBytes(szCipherText);

            int iLen = szText.GetLength(0);
            int i = 0;

            string st = "";
            byte[] t2 = { 0, 0, 0, 0 };

            byte[] bDecrypt = new byte[iLen / 2];
            int index = 0;
            while (i < iLen)
            {
                memset(m_szIn, 0, 16);
                memset(m_szOut, 0, 16);
                for (int j = 0, k = 0; j < 32; j += 2, k++)
                {
                    t2[0] = szText[i + j];
                    t2[2] = szText[i + j + 1];
                    st = encoW.GetString(t2);
                    m_szIn[k] = System.Convert.ToByte(st, 16);
                }

                XOR(false);

                KeyExpansion();
                InvCipher();

                for (int j = 0; j < Nb * 4; j++)
                {
                    bDecrypt[index] = m_szOut[j];
                    index++;
                }
                i += 32;
            }

            while (bDecrypt[index - 1] == 0 && bDecrypt[index - 2] == 0)
            {
                index -= 2;
                if (index == 0) return "";
            }

            byte[] dszDecode = new byte[index];
            for (i = 0; i < index; ++i) dszDecode[i] = bDecrypt[i];

            return encoW.GetString(dszDecode);
        }

        private void memset(byte[] szIn, byte b, int nSize)
        {
            for (int i = 0; i < nSize; ++i) szIn[i] = b;
        }
    }
}
