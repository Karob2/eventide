using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventide.Input
{
    public enum GameCommand
    {
        Up,
        Down,
        Left,
        Right,
        Action1,
        Action2,
        MenuUp,
        MenuDown,
        MenuLeft,
        MenuRight,
        MenuConfirm,
        MenuCancel,
        Console,
        ConsoleConfirm,
        ConsoleCancel
    }

    public enum UniversalInputType
    {
        Keyboard,
        Mouse, //TODO: Touch?
        Gamepad
    }

    /*
    public enum KeyModifiers
    {
        LeftControl = 1,
        RightControl = 2,
        LeftShift = 4,
        RightShift = 8,
        LeftAlt = 16,
        RightAlt = 32
    }
    */

    public enum GamepadInput
    {
        A,
        B,
        X,
        Y,
        LB,
        RB,
        LT,
        RT,
        Start,
        Back,
        Big,
        Up,
        Down,
        Left,
        Right,
        LStick,
        RStick,
        LUp,
        LDown,
        LLeft,
        LRight,
        RUp,
        RDown,
        RLeft,
        RRight
    }

    public enum MouseInput
    {
        Up,
        Down,
        Left,
        Right,
        LButton,
        MButton,
        RButton,
        X1Button,
        X2Button,
        ScrollUp,
        ScrollDown
    }
}
