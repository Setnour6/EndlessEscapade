using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace EEMod.UI.Elements
{
    internal class DragableUIPanel : UIPanel
    {
        private bool dragging;
        private float lastX, lastY;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (dragging)
            {
                Top.Pixels = Helpers.Clamp(Top.Pixels - (lastY - Main.mouseY), 0, Parent.Height.Pixels - Height.Pixels);
                Left.Pixels = Helpers.Clamp(Left.Pixels - (lastX - Main.mouseX), 0, Parent.Width.Pixels - Width.Pixels);
                lastX = Main.mouseX;
                lastY = Main.mouseY;
                Recalculate();
            }
        }
        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
            var d = evt.MousePosition;
            lastX = d.X;
            lastY = d.Y;
            dragging = true;
        }

        public override void LeftMouseUp(UIMouseEvent evt)
        {
            base.LeftMouseUp(evt);
            dragging = false;
            Recalculate();
        }
    }
}