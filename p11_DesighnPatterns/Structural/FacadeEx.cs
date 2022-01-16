using System;

namespace p11_DesighnPatterns.Structural
{
    public class FacadeEx
    {
        public static void Test()
        {
            var mBuider = new MotivatorFacade.MotivatorBuilder();
            Console.WriteLine(mBuider
                .SetFrame()
                .SetBackgroung()
                .SetPicture()
                .SetText()
                .Build()
                .CreateMotivator("","impossible is nothing"));
        }
    }

    public class MotivatorFacade
    {
        private readonly Frame _frame;
        private readonly Picture _picture;
        private readonly Background _background;
        private readonly Text _msg;

        private MotivatorFacade(Frame frame, Picture picture, Background background, Text msg)
        {
            _frame = frame;
            _picture = picture;
            _background = background;
            _msg = msg;
        }

        public class MotivatorBuilder
        {
            private Frame _frame;
            private Picture _picture;
            private Background _background;
            private Text _msg;
            
            public MotivatorBuilder() { }

            public MotivatorBuilder SetFrame()
            {
                _frame = new Frame();
                return this;
            }
            
            public MotivatorBuilder SetBackgroung()
            {
                _background = new Background();
                return this;
            }
            
            public MotivatorBuilder SetPicture()
            {
                _picture = new Picture();
                return this;
            }
            
            public MotivatorBuilder SetText()
            {
                _msg = new Text();
                return this;
            }

            public MotivatorFacade Build() => new MotivatorFacade(_frame, _picture, _background, _msg);
        }
        
        public string CreateMotivator(string image, string message)
        {
            image = string.Join(',', image, _frame.CreateFrame(), _background.CreateBackground(),
                _picture.CreatePicture(), _msg.CreateText(message));
            return image;
        }
    }

    public class Frame
    {
        public string CreateFrame()
        {
            Console.WriteLine("Frame Created");
            return "frame";
        }
    }

    public class Background
    {
        public string CreateBackground()
        {
            Console.WriteLine("Background created");
            return "background";
        }
    }

    public class Picture
    {
        public string CreatePicture()
        {
            Console.WriteLine("Picture created");
            return "picture";
        }
    }

    public class Text
    {
        public string CreateText(string text)
        {
            Console.WriteLine("Text created");
            return text;
        }
    }
}