using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bits_Please____Bullet_Hell.Player_Init;
using System.Threading;
using Microsoft.Xna.Framework.Content;

namespace Bits_Please____Bullet_Hell.UI_Control
{
    public struct Text
    {
        public int key;
        public string myText;
        public Rectangle rect;
        public string strplaceOfUse;
        public bool ShouldItBeShownInThisScreen;
        public int HorizontalMargin;
        public string Action;
        public bool isFocused;
    }
    public struct mylist
    {
        public string mystring;
        public int key;
    }

    public class clsInput
    {
        public string AKeyState = "none";
        public string BKeyState = "none";
        public string CKeyState = "none";
        public string DKeyState = "none";
        public string EKeyState = "none";
        public string FKeyState = "none";
        public string GKeyState = "none";
        public string HKeyState = "none";
        public string IKeyState = "none";
        public string JKeyState = "none";
        public string KKeyState = "none";
        public string LKeyState = "none";
        public string MKeyState = "none";
        public string NKeyState = "none";
        public string OKeyState = "none";
        public string PKeyState = "none";
        public string QKeyState = "none";
        public string RKeyState = "none";
        public string SKeyState = "none";
        public string TKeyState = "none";
        public string UKeyState = "none";
        public string VKeyState = "none";
        public string WKeyState = "none";
        public string XKeyState = "none";
        public string YKeyState = "none";
        public string ZKeyState = "none";
        public string SpaceKeyState = "none";
        public string BackKeyState = "none";
        /// //////////////////////////////////////////////
        public string OemSemicolonKeyState = "none";
        public string OemQuotesKeyState = "none";
        public string OemCommaKeyState = "none";
        public string OemPeriodKeyState = "none";
        public string OemQuestionKeyState = "none";
        /////////////////////////////////////////////////
        public string OemOpenBracketsKeyState = "none";
        public string OemCloseBracketsKeyState = "none";
        public string OemPipeKeyState = "none";
        //////////////////////////////////////////////////
        public string D1KeyState = "none";
        public string D2KeyState = "none";
        public string D3KeyState = "none";
        public string D4KeyState = "none";
        public string D5KeyState = "none";
        public string D6KeyState = "none";
        public string D7KeyState = "none";
        public string D8KeyState = "none";
        public string D9KeyState = "none";
        public string D0KeyState = "none";
        //////////////////////////////////////////////////
        public string OemTildeKeyState = "none";
        public string OemMinusKeyState = "none";
        public string OemPlusKeyState = "none";
        /// ////////////////////////////////////////////////
        public bool isShiftPressed(Keys[] mykeys)
        {
            bool result = false;
            foreach (Keys key in mykeys)
            {
                if (key == Keys.LeftShift || key == Keys.RightShift)
                {
                    result = true;
                }
            }
            return result;
        }
        public bool isControlPressed(Keys[] mykeys)
        {
            bool result = false;
            foreach (Keys key in mykeys)
            {
                if (key == Keys.LeftControl || key == Keys.RightControl)
                {
                    result = true;
                }
            }
            return result;
        }
    }

    class TextBox
    {
        public bool IsGettingStringFromUseron = false;
        public string keyboardString = "";
        public clsInput myclsinput = new clsInput();
        SpriteFont _font;

        public TextBox(SpriteFont font)
        {
            _font = font;
        }

        public char OnKeyboardKeyPress(KeyboardState myKeyboardState)
        {
            if (IsGettingStringFromUseron)
            {
                Keys[] mykeys = myKeyboardState.GetPressedKeys();
                bool isShift = myclsinput.isShiftPressed(mykeys);
                bool isControl = myclsinput.isControlPressed(mykeys);
                if (myKeyboardState.IsKeyDown(Keys.A))
                {
                    if (myclsinput.AKeyState == "none") myclsinput.AKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.A))
                {
                    if (myclsinput.AKeyState == "down")
                    {
                        myclsinput.AKeyState = "none";
                        if (isShift) return 'A';
                        else return 'a';
                    }
                }
                ////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.Space))
                {
                    if (myclsinput.SpaceKeyState == "none") myclsinput.SpaceKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.Space))
                {
                    if (myclsinput.SpaceKeyState == "down")
                    {
                        myclsinput.SpaceKeyState = "none";
                        return ' ';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.B))
                {
                    if (myclsinput.BKeyState == "none") myclsinput.BKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.B))
                {
                    if (myclsinput.BKeyState == "down")
                    {
                        myclsinput.BKeyState = "none";
                        if (isShift) return 'B';
                        else return 'b';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.Z))
                {
                    if (myclsinput.ZKeyState == "none") myclsinput.ZKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.Z))
                {
                    if (myclsinput.ZKeyState == "down")
                    {
                        myclsinput.ZKeyState = "none";
                        if (isShift) return 'Z';
                        else return 'z';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.C))
                {
                    if (myclsinput.CKeyState == "none") myclsinput.CKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.C))
                {
                    if (myclsinput.CKeyState == "down")
                    {
                        myclsinput.CKeyState = "none";
                        if (isShift) return 'C';
                        else return 'c';
                    }
                }
                ////////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.D))
                {
                    if (myclsinput.DKeyState == "none") myclsinput.DKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.D))
                {
                    if (myclsinput.DKeyState == "down")
                    {
                        myclsinput.DKeyState = "none";
                        if (isShift) return 'D';
                        else return 'd';
                    }
                }
                ////////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.E))
                {
                    if (myclsinput.EKeyState == "none") myclsinput.EKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.E))
                {
                    if (myclsinput.EKeyState == "down")
                    {
                        myclsinput.EKeyState = "none";
                        if (isShift) return 'E';
                        else return 'e';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.F))
                {
                    if (myclsinput.FKeyState == "none") myclsinput.FKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.F))
                {
                    if (myclsinput.FKeyState == "down")
                    {
                        myclsinput.FKeyState = "none";
                        if (isShift) return 'F';
                        else return 'f';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.G))
                {
                    if (myclsinput.GKeyState == "none") myclsinput.GKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.G))
                {
                    if (myclsinput.GKeyState == "down")
                    {
                        myclsinput.GKeyState = "none";
                        if (isShift) return 'G';
                        else return 'g';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.H))
                {
                    if (myclsinput.HKeyState == "none") myclsinput.HKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.H))
                {
                    if (myclsinput.HKeyState == "down")
                    {
                        myclsinput.HKeyState = "none";
                        if (isShift) return 'H';
                        else return 'h';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.I))
                {
                    if (myclsinput.IKeyState == "none") myclsinput.IKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.I))
                {
                    if (myclsinput.IKeyState == "down")
                    {
                        myclsinput.IKeyState = "none";
                        if (isShift) return 'I';
                        else return 'i';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.J))
                {
                    if (myclsinput.JKeyState == "none") myclsinput.JKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.J))
                {
                    if (myclsinput.JKeyState == "down")
                    {
                        myclsinput.JKeyState = "none";
                        if (isShift) return 'J';
                        else return 'j';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.K))
                {
                    if (myclsinput.KKeyState == "none") myclsinput.KKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.K))
                {
                    if (myclsinput.KKeyState == "down")
                    {
                        myclsinput.KKeyState = "none";
                        if (isShift) return 'K';
                        else return 'k';
                    }
                }
                if (myKeyboardState.IsKeyDown(Keys.L))
                {
                    if (myclsinput.LKeyState == "none") myclsinput.LKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.L))
                {
                    if (myclsinput.LKeyState == "down")
                    {
                        myclsinput.LKeyState = "none";
                        if (isShift) return 'L';
                        else return 'l';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.M))
                {
                    if (myclsinput.MKeyState == "none") myclsinput.MKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.M))
                {
                    if (myclsinput.MKeyState == "down")
                    {
                        myclsinput.MKeyState = "none";
                        if (isShift) return 'M';
                        else return 'm';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.N))
                {
                    if (myclsinput.NKeyState == "none") myclsinput.NKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.N))
                {
                    if (myclsinput.NKeyState == "down")
                    {
                        myclsinput.NKeyState = "none";
                        if (isShift) return 'N';
                        else return 'n';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.O))
                {
                    if (myclsinput.OKeyState == "none") myclsinput.OKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.O))
                {
                    if (myclsinput.OKeyState == "down")
                    {
                        myclsinput.OKeyState = "none";
                        if (isShift) return 'O';
                        else return 'o';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.P))
                {
                    if (myclsinput.PKeyState == "none") myclsinput.PKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.P))
                {
                    if (myclsinput.PKeyState == "down")
                    {
                        myclsinput.PKeyState = "none";
                        if (isShift) return 'P';
                        else return 'p';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.Q))
                {
                    if (myclsinput.QKeyState == "none") myclsinput.QKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.Q))
                {
                    if (myclsinput.QKeyState == "down")
                    {
                        myclsinput.QKeyState = "none";
                        if (isShift) return 'Q';
                        else return 'q';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.R))
                {
                    if (myclsinput.RKeyState == "none") myclsinput.RKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.R))
                {
                    if (myclsinput.RKeyState == "down")
                    {
                        myclsinput.RKeyState = "none";
                        if (isShift) return 'R';
                        else return 'r';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.S))
                {
                    if (myclsinput.SKeyState == "none") myclsinput.SKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.S))
                {
                    if (myclsinput.SKeyState == "down")
                    {
                        myclsinput.SKeyState = "none";
                        if (isShift) return 'S';
                        else return 's';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.T))
                {
                    if (myclsinput.TKeyState == "none") myclsinput.TKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.T))
                {
                    if (myclsinput.TKeyState == "down")
                    {
                        myclsinput.TKeyState = "none";
                        if (isShift) return 'T';
                        else return 't';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.U))
                {
                    if (myclsinput.UKeyState == "none") myclsinput.UKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.U))
                {
                    if (myclsinput.UKeyState == "down")
                    {
                        myclsinput.UKeyState = "none";
                        if (isShift) return 'U';
                        else return 'u';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.V))
                {
                    if (myclsinput.VKeyState == "none") myclsinput.VKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.V))
                {
                    if (myclsinput.VKeyState == "down")
                    {
                        myclsinput.VKeyState = "none";
                        if (isShift) return 'V';
                        else return 'v';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.W))
                {
                    if (myclsinput.WKeyState == "none") myclsinput.WKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.W))
                {
                    if (myclsinput.WKeyState == "down")
                    {
                        myclsinput.WKeyState = "none";
                        if (isShift) return 'W';
                        else return 'w';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.Y))
                {
                    if (myclsinput.YKeyState == "none") myclsinput.YKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.Y))
                {
                    if (myclsinput.YKeyState == "down")
                    {
                        myclsinput.YKeyState = "none";
                        if (isShift) return 'Y';
                        else return 'y';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.X))
                {
                    if (myclsinput.XKeyState == "none") myclsinput.XKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.X))
                {
                    if (myclsinput.XKeyState == "down")
                    {
                        myclsinput.XKeyState = "none";
                        if (isShift) return 'X';
                        else return 'x';
                    }
                }
                /////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.OemSemicolon))
                {
                    if (myclsinput.OemSemicolonKeyState == "none") myclsinput.OemSemicolonKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.OemSemicolon))
                {
                    if (myclsinput.OemSemicolonKeyState == "down")
                    {
                        myclsinput.OemSemicolonKeyState = "none";
                        if (isShift) return ':';
                        else return ';';
                    }
                }
                /////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.OemQuotes))
                {
                    if (myclsinput.OemQuotesKeyState == "none") myclsinput.OemQuotesKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.OemQuotes))
                {
                    if (myclsinput.OemQuotesKeyState == "down")
                    {
                        myclsinput.OemQuotesKeyState = "none";
                        if (isShift) return '\"';
                        else return '\'';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.Back))
                {
                    if (myclsinput.BackKeyState == "none") myclsinput.BackKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.Back))
                {
                    if (myclsinput.BackKeyState == "down")
                    {
                        if (keyboardString != "") keyboardString = keyboardString.Remove(keyboardString.Length - 1);
                        myclsinput.BackKeyState = "none";
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.OemComma))
                {
                    if (myclsinput.OemCommaKeyState == "none") myclsinput.OemCommaKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.OemComma))
                {
                    if (myclsinput.OemCommaKeyState == "down")
                    {
                        myclsinput.OemCommaKeyState = "none";
                        if (isShift) return '<';
                        else return ',';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.OemPeriod))
                {
                    if (myclsinput.OemPeriodKeyState == "none") myclsinput.OemPeriodKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.OemPeriod))
                {
                    if (myclsinput.OemPeriodKeyState == "down")
                    {
                        myclsinput.OemPeriodKeyState = "none";
                        if (isShift) return '>';
                        else return '.';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.OemQuestion))
                {
                    if (myclsinput.OemQuestionKeyState == "none") myclsinput.OemQuestionKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.OemQuestion))
                {
                    if (myclsinput.OemQuestionKeyState == "down")
                    {
                        myclsinput.OemQuestionKeyState = "none";
                        if (isShift) return '?';
                        else return '/';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.OemOpenBrackets))
                {
                    if (myclsinput.OemOpenBracketsKeyState == "none") myclsinput.OemOpenBracketsKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.OemOpenBrackets))
                {
                    if (myclsinput.OemOpenBracketsKeyState == "down")
                    {
                        myclsinput.OemOpenBracketsKeyState = "none";
                        if (isShift) return '{';
                        else return '[';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.OemCloseBrackets))
                {
                    if (myclsinput.OemCloseBracketsKeyState == "none") myclsinput.OemCloseBracketsKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.OemCloseBrackets))
                {
                    if (myclsinput.OemCloseBracketsKeyState == "down")
                    {
                        myclsinput.OemCloseBracketsKeyState = "none";
                        if (isShift) return '}';
                        else return ']';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.OemPipe))
                {
                    if (myclsinput.OemPipeKeyState == "none") myclsinput.OemPipeKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.OemPipe))
                {
                    if (myclsinput.OemPipeKeyState == "down")
                    {
                        myclsinput.OemPipeKeyState = "none";
                        if (isShift) return '|';
                        else return '\\';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.D1))
                {
                    if (myclsinput.D1KeyState == "none") myclsinput.D1KeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.D1))
                {
                    if (myclsinput.D1KeyState == "down")
                    {
                        myclsinput.D1KeyState = "none";
                        if (isShift) return '!';
                        else return '1';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.D2))
                {
                    if (myclsinput.D2KeyState == "none") myclsinput.D2KeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.D2))
                {
                    if (myclsinput.D2KeyState == "down")
                    {
                        myclsinput.D2KeyState = "none";
                        if (isShift) return '@';
                        else return '2';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.D3))
                {
                    if (myclsinput.D3KeyState == "none") myclsinput.D3KeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.D3))
                {
                    if (myclsinput.D3KeyState == "down")
                    {
                        myclsinput.D3KeyState = "none";
                        if (isShift) return '#';
                        else return '3';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.D4))
                {
                    if (myclsinput.D4KeyState == "none") myclsinput.D4KeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.D4))
                {
                    if (myclsinput.D4KeyState == "down")
                    {
                        myclsinput.D4KeyState = "none";
                        if (isShift) return '$';
                        else return '4';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.D5))
                {
                    if (myclsinput.D5KeyState == "none") myclsinput.D5KeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.D5))
                {
                    if (myclsinput.D5KeyState == "down")
                    {
                        myclsinput.D5KeyState = "none";
                        if (isShift) return '%';
                        else return '5';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.D6))
                {
                    if (myclsinput.D6KeyState == "none") myclsinput.D6KeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.D6))
                {
                    if (myclsinput.D6KeyState == "down")
                    {
                        myclsinput.D6KeyState = "none";
                        if (isShift) return '^';
                        else return '6';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.D7))
                {
                    if (myclsinput.D7KeyState == "none") myclsinput.D7KeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.D7))
                {
                    if (myclsinput.D7KeyState == "down")
                    {
                        myclsinput.D7KeyState = "none";
                        if (isShift) return '&';
                        else return '7';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.D8))
                {
                    if (myclsinput.D8KeyState == "none") myclsinput.D8KeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.D8))
                {
                    if (myclsinput.D8KeyState == "down")
                    {
                        myclsinput.D8KeyState = "none";
                        if (isShift) return '*';
                        else return '8';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.D9))
                {
                    if (myclsinput.D9KeyState == "none") myclsinput.D9KeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.D9))
                {
                    if (myclsinput.D9KeyState == "down")
                    {
                        myclsinput.D9KeyState = "none";
                        if (isShift) return '(';
                        else return '9';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.D0))
                {
                    if (myclsinput.D0KeyState == "none") myclsinput.D0KeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.D0))
                {
                    if (myclsinput.D0KeyState == "down")
                    {
                        myclsinput.D0KeyState = "none";
                        if (isShift) return ')';
                        else return '0';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.OemTilde))
                {
                    if (myclsinput.OemTildeKeyState == "none") myclsinput.OemTildeKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.OemTilde))
                {
                    if (myclsinput.OemTildeKeyState == "down")
                    {
                        myclsinput.OemTildeKeyState = "none";
                        if (isShift) return '~';
                        else return '`';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.OemMinus))
                {
                    if (myclsinput.OemMinusKeyState == "none") myclsinput.OemMinusKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.OemMinus))
                {
                    if (myclsinput.OemMinusKeyState == "down")
                    {
                        myclsinput.OemMinusKeyState = "none";
                        if (isShift) return '_';
                        else return '-';
                    }
                }
                ///////////////////////////////////////////////////////////
                if (myKeyboardState.IsKeyDown(Keys.OemPlus))
                {
                    if (myclsinput.OemPlusKeyState == "none") myclsinput.OemPlusKeyState = "down";
                }
                if (myKeyboardState.IsKeyUp(Keys.OemPlus))
                {
                    if (myclsinput.OemPlusKeyState == "down")
                    {
                        myclsinput.OemPlusKeyState = "none";
                        if (isShift) return '+';
                        else return '=';
                    }
                }
                ///////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////
            }
            return '\0';
        }
    }
}
