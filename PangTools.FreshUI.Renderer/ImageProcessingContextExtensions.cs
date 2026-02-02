using PangTools.FreshUI.Serialization.DTO;
using SixLabors.ImageSharp;
using PangTools.FreshUI.Serialization.Models;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace PangTools.FreshUI.Renderer;

public static class ImageProcessingContextExtensions
{
    public static IImageProcessingContext DrawElement(this IImageProcessingContext ctx, Element element, FileAtlas fileAtlas, FrameInfoAtlas frameInfoAtlas,
        bool drawOutline = false)
    {
        int[] sizes = element.Size.Split(" ").Select(Int32.Parse).ToArray();
        int width = sizes[0];
        int height = sizes[1];

        ctx.DrawFrame(element, fileAtlas, frameInfoAtlas);
        
        if (drawOutline)
        {
            ctx.Draw(Color.Red, 1f, new RectangleF(0, 0,width,height));
        }

        return ctx;
    }
    
    public static IImageProcessingContext DrawArea(this IImageProcessingContext ctx, Item areaItem, FileAtlas fileAtlas, bool drawOutline = false)
    {
        int[]? areaItemRect = areaItem.Rectangle?.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Select(Int32.Parse).ToArray();

        if (areaItemRect == null || areaItemRect.Length != 4)
        {
            return ctx;
        }

        Parameter? bgParam = areaItem.Parameters.FirstOrDefault(p => p.Name.Equals("bgimg"));
        Parameter? stretchParam = areaItem.GetParameter("stretch");
        Parameter? visibleParam = areaItem.GetParameter("visible");

        if (visibleParam != null && visibleParam.Value == "0")
        {
            return ctx;
        }

        if (bgParam != null && bgParam.Value.Length > 0)
        {
            Image? bgTexture = fileAtlas.GetImage($"{bgParam.Value}.");

            if (bgTexture != null)
            {
                if (stretchParam != null && stretchParam.Value == "1")
                {
                    bgTexture.Mutate(bgCtx =>
                    {
                        bgCtx.Resize(new Size(
                            areaItemRect[2] - areaItemRect[0],
                            areaItemRect[3] - areaItemRect[1]
                        ));
                    });
                }
                
                ctx.DrawImage(bgTexture, new Point(areaItemRect[0], areaItemRect[1]), 1f); 
            }
        }

        if (drawOutline)
        {
            ctx.Draw(Color.LightBlue, 1f, new RectangleF(areaItemRect[0], areaItemRect[1], areaItemRect[2], areaItemRect[3]));
        }
        
        return ctx;
    }

    public static IImageProcessingContext DrawButton(this IImageProcessingContext ctx, Item buttonItem, string buttonState, FileAtlas fileAtlas)
    {
        int[] pos = buttonItem.Position.Split(" ").Select(Int32.Parse).ToArray();
            
        Parameter? stateParam = buttonItem.Parameters.FirstOrDefault(p => p.Name.Equals(buttonState));

        if (stateParam != null)
        {
            Image? normalTexture = fileAtlas.GetImage($"{stateParam.Value}.");

            if (normalTexture != null)
            {
                ctx.DrawImage(normalTexture, new Point(pos[0], pos[1]), 1f);    
            }
        }

        return ctx;
    }

    public static IImageProcessingContext DrawFrame(this IImageProcessingContext ctx, Element element, FileAtlas fileAtlas, FrameInfoAtlas frameInfoAtlas, string frameType = "bfrm")
    {
        int[] sizes = element.Size.Split(" ").Select(Int32.Parse).ToArray();
        int width = sizes[0];
        int height = sizes[1];
        
        Dictionary<string, FrameInfo> frameInfo = frameInfoAtlas[element.Resource];
        if (frameInfo.ContainsKey(frameType))
        {
            FrameInfo borderFrameInfo = frameInfo[frameType];

            Image? topLeftImage = fileAtlas.GetImage(borderFrameInfo.FileNames[0]);
            Image? topCenterImage = fileAtlas.GetImage(borderFrameInfo.FileNames[1]);
            Image? topRightImage = fileAtlas.GetImage(borderFrameInfo.FileNames[2]);
            Image? centerLeftImage = fileAtlas.GetImage(borderFrameInfo.FileNames[3]);
            Image? centerImage = fileAtlas.GetImage(borderFrameInfo.FileNames[4]);
            Image? centerRightImage = fileAtlas.GetImage(borderFrameInfo.FileNames[5]);
            Image? bottomLeftImage = fileAtlas.GetImage(borderFrameInfo.FileNames[6]);
            Image? bottomCenterImage = fileAtlas.GetImage(borderFrameInfo.FileNames[7]);
            Image? bottomRightImage = fileAtlas.GetImage(borderFrameInfo.FileNames[8]);

            if (topLeftImage != null)
            {
                ctx.DrawImage(topLeftImage, new Point(0, 0), 1f);
            }

            if (topCenterImage != null)
            {
                topCenterImage.Mutate(imgCtx => imgCtx.Resize(new Size(
                        width - topLeftImage.Width - topRightImage.Width,
                        topCenterImage.Height
                    )));

                ctx.DrawImage(topCenterImage, new Point(topLeftImage.Width, 0), 1f);
            }
            
            if (topRightImage != null)
            {
                ctx.DrawImage(topRightImage, new Point(width - topRightImage.Width, 0), 1f);
            }
            
            if (centerLeftImage != null)
            {
                centerLeftImage.Mutate(imgCtx => imgCtx.Resize(new Size(
                    centerLeftImage.Width,
                    height - topLeftImage.Height - bottomLeftImage.Height
                )));

                ctx.DrawImage(centerLeftImage, new Point(0, topLeftImage.Height), 1f);
            }

            if (centerImage != null)
            {
                centerImage.Mutate(imgCtx => imgCtx.Resize(new Size(
                    width - centerLeftImage.Width - centerRightImage.Width,
                    height - topLeftImage.Height - bottomLeftImage.Height
                )));

                ctx.DrawImage(centerImage, new Point(topLeftImage.Width, topLeftImage.Height), 1f);
            }
            
            if (centerRightImage != null)
            {
                centerRightImage.Mutate(imgCtx => imgCtx.Resize(new Size(
                    centerRightImage.Width,
                    height - topRightImage.Height - bottomRightImage.Height
                )));

                ctx.DrawImage(centerRightImage, new Point(width - centerRightImage.Width, topRightImage.Height), 1f);
            }
            
            if (bottomLeftImage != null)
            {
                ctx.DrawImage(bottomLeftImage, new Point(0, height - bottomLeftImage.Height), 1f);
            }
            
            if (bottomCenterImage != null)
            {
                bottomCenterImage.Mutate(imgCtx => imgCtx.Resize(new Size(
                    width - bottomLeftImage.Width - bottomRightImage.Width,
                    bottomCenterImage.Height
                )));

                ctx.DrawImage(bottomCenterImage, new Point(bottomLeftImage.Width, height - bottomCenterImage.Height), 1f);
            }
            
            if (bottomRightImage != null)
            {
                ctx.DrawImage(bottomRightImage, new Point(width - bottomRightImage.Width, height - bottomRightImage.Height), 1f);
            }
        }

        return ctx;
    }
}