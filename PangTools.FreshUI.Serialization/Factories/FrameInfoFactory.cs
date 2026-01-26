using PangTools.FreshUI.Serialization.DTO;
using PangTools.FreshUI.Serialization.Models;

namespace PangTools.FreshUI.Serialization.Factories;

public static class FrameInfoFactory
{
    public static FrameInfoAtlas BuildFrameInfo(List<Element> frameElements)
    {
        FrameInfoAtlas dict = new();
        
        foreach (Element element in frameElements)
        {
            Dictionary<string, FrameInfo> elementDict = new();
            
            if (element.Type != "FRAME")
            {
                continue;
            }

            if (element.BorderFrame != null)
            {
                string[] borderFileNames = new string[9];
                
                for (int i = 0; i < 9; i++)
                {
                    borderFileNames[i] = $"{element.BorderFrame.FileName}0{i}";
                }
                
                elementDict.Add("bfrm", new FrameInfo() { FileNames = borderFileNames });
            }
            
            if (element.ClearFrame != null)
            {
                string[] clearFileNames = new string[10];
                
                for (int i = 0; i <= 9; i++)
                {
                    clearFileNames[i] = $"{element.ClearFrame.FileName}0{i}";
                }
                
                elementDict.Add("cfrm", new FrameInfo() { FileNames = clearFileNames });
            }
            
            if (element.SquareFrame != null)
            {
                string[] squareFileNames = new string[9];
                
                for (int i = 0; i < 9; i++)
                {
                    squareFileNames[i] = $"{element.SquareFrame.FileName}0{i}";
                }
                
                elementDict.Add("sfrm", new FrameInfo() { FileNames = squareFileNames });
            }
            
            dict.Add(element.Name, elementDict);
        }

        return dict;
    }
}