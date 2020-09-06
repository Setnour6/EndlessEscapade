using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using EEMod.Projectiles.Melee;
using EEMod.Projectiles.Mage;
using Terraria.ModLoader;
using EEMod.Extensions;
using EEMod.Projectiles.Summons;
using EEMod.Projectiles;
using EEMod.Projectiles.Runes;

namespace EEMod.Effects
{
    public class TrailManager
    {
        private List<Trail> _trails = new List<Trail>();
        private Effect _effect;
        private BasicEffect _basicEffect;

        public TrailManager(Mod mod)
        {
            _trails = new List<Trail>();
            _effect = mod.GetEffect("Effects/trailShaders");
            _basicEffect = new BasicEffect(Main.graphics.GraphicsDevice);
            _basicEffect.VertexColorEnabled = true;
        }

        public void DoTrailCreation(Projectile projectile)
        {
            Mod mod = EEMod.instance;
            if (projectile.type == ModContent.ProjectileType<FeatheredChakramProjectileAlt>() || projectile.type == ModContent.ProjectileType<AkumoMinionProjectile>() || projectile.type == ModContent.ProjectileType<FeatheredDreamcatcherProjectile>())
            {
                CreateTrail(projectile, new StandardColorTrail(new Color(200, 98, 50)), new RoundCap(), new SleepingStarTrailPosition(), 8f, 250f);
            }
            if (projectile.type == ModContent.ProjectileType<SpiritPistolProjectileSecondary>())
            {
                CreateTrail(projectile, new StandardColorTrail(new Color(97, 215, 248)), new RoundCap(), new SleepingStarTrailPosition(), 10f, 250f);
            }
            if (projectile.type == ModContent.ProjectileType<HydrofluoricStaffProjectile>())
            {
                CreateTrail(projectile, new StandardColorTrail(new Color(111, 235, 124)), new RoundCap(), new SleepingStarTrailPosition(), 12f, 400f);
            }
            if (projectile.type == ModContent.ProjectileType<WaterDragonsBubble>() || projectile.type == ModContent.ProjectileType<BubblingWatersBubbleSmall>())
            {
                CreateTrail(projectile, new StandardColorTrail(new Color(97, 215, 248)), new RoundCap(), new SleepingStarTrailPosition(), 8f,128f);
            }
        }

        public void TryTrailKill(Projectile projectile)
        {

        }

        public void CreateTrail(Projectile projectile, ITrailColor trailType, ITrailCap trailCap, ITrailPosition trailPosition, float widthAtFront, float maxLength, ITrailShader shader = null)
        {
            Trail newTrail = new Trail(projectile, trailType, trailCap, trailPosition, shader == null ? new DefaultShader() : shader, widthAtFront, maxLength);
            newTrail.Update();
            _trails.Add(newTrail);
        }

        public void UpdateTrails()
        {
            for (int i = 0; i < _trails.Count; i++)
            {
                Trail trail = _trails[i];

                trail.Update();
                if (trail.Dead)
                {
                    _trails.RemoveAt(i);
                    i--;
                }
            }
        }

        public void DrawTrails(SpriteBatch spriteBatch)
        {
            foreach (Trail trail in _trails)
            {
                trail.Draw(_effect, _basicEffect, spriteBatch.GraphicsDevice);
            }
        }

        public void TryEndTrail(Projectile projectile, float dissolveSpeed)
        {
            for (int i = 0; i < _trails.Count; i++)
            {
                Trail trail = _trails[i];

                if (trail.MyProjectile.whoAmI == projectile.whoAmI)
                {
                    trail.StartDissolve(dissolveSpeed);
                    return;
                }
            }
        }
    }

    public class Trail
    {
        public Projectile MyProjectile { get; private set; }
        public bool Dead { get; private set; }

        private int _originalProjectileType;

        private ITrailCap _trailCap;
        private ITrailColor _trailColor;
        private ITrailPosition _trailPosition;
        private ITrailShader _trailShader;
        private float _widthStart;

        private float _currentLength;
        private float _maxLength;

        private List<Vector2> _points;

        private bool _dissolving;
        private float _dissolveSpeed;
        private float _originalMaxLength;
        private float _originalWidth;

        public Trail(Projectile projectile, ITrailColor type, ITrailCap cap, ITrailPosition position, ITrailShader shader, float widthAtFront, float maxLength)
        {
            MyProjectile = projectile;
            Dead = false;

            _trailCap = cap;
            _trailColor = type;
            _trailPosition = position;
            _trailShader = shader;
            _maxLength = maxLength;
            _widthStart = widthAtFront;

            _originalProjectileType = projectile.type;
            _points = new List<Vector2>();
        }

        public void StartDissolve(float speed)
        {
            _dissolving = true;
            _dissolveSpeed = speed;
            _originalWidth = _widthStart;
            _originalMaxLength = _maxLength;
        }

        public void Update()
        {
            if (_dissolving)
            {
                _maxLength -= _dissolveSpeed;
                _widthStart = (_maxLength / _originalMaxLength) * _originalWidth;
                if (_maxLength <= 0f)
                {
                    Dead = true;
                    return;
                }

                TrimToLength(_maxLength);
                return;
            }

            if (!MyProjectile.active || MyProjectile.type != _originalProjectileType)
            {
                StartDissolve(_maxLength / 10f);
            }

            Vector2 thisPoint = _trailPosition.GetNextTrailPosition(MyProjectile);

            if (_points.Count == 0)
            {
                _points.Add(thisPoint);
                return;
            }

            float distance = Vector2.Distance(thisPoint, _points[0]);
            _points.Insert(0, thisPoint);

            //If adding the next point is too much
            if (_currentLength + distance > _maxLength)
            {
                TrimToLength(_maxLength);
            }
            else
            {
                _currentLength += distance;
            }
        }

        private void TrimToLength(float length)
        {
            if (_points.Count == 0) return;

            _currentLength = length;

            int firstPointOver = -1;
            float newLength = 0;

            for (int i = 1; i < _points.Count; i++)
            {
                newLength += Vector2.Distance(_points[i], _points[i - 1]);
                if (newLength > length)
                {
                    firstPointOver = i;
                    break;
                }
            }

            if (firstPointOver == -1) return;

            //get new end point based on remaining distance
            float leftOverLength = newLength - length;
            Vector2 between = _points[firstPointOver] - _points[firstPointOver - 1];
            float newPointDistance = between.Length() - leftOverLength;
            between.Normalize();

            int toRemove = _points.Count - firstPointOver;
            _points.RemoveRange(firstPointOver, toRemove);

            _points.Add(_points.Last() + between * newPointDistance);
        }

        public void Draw(Effect effect, BasicEffect effect2, GraphicsDevice device)
        {
            if (Dead) return;
            if (_points.Count <= 1) return;

            //calculate trail's length
            float trailLength = 0f;
            for (int i = 1; i < _points.Count; i++)
            {
                trailLength += Vector2.Distance(_points[i - 1], _points[i]);
            }

            //Create vertice array, needs to be equal to the number of quads * 6 (each quad has two tris, which are 3 vertices)
            int currentIndex = 0;
            VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[(_points.Count - 1) * 6 + _trailCap.ExtraTris * 3];

            //method to make it look less horrible
            void AddVertex(Vector2 position, Color color, Vector2 uv)
            {
                vertices[currentIndex++] = new VertexPositionColorTexture(new Vector3(position.ForDraw(), 0f), color, uv);
            }

            float currentDistance = 0f;
            float halfWidth = _widthStart * 0.5f;

            Vector2 startNormal = CurveNormal(_points, 0);
            Vector2 prevClockwise = _points[0] + startNormal * halfWidth;
            Vector2 prevCClockwise = _points[0] - startNormal * halfWidth;

            Color previousColor = _trailColor.GetColourAt(0f, trailLength, _points);

            _trailCap.AddCap(vertices, ref currentIndex, previousColor, _points[0], startNormal, _widthStart);

            for (int i = 1; i < _points.Count; i++)
            {
                currentDistance += Vector2.Distance(_points[i - 1], _points[i]);

                float thisPointsWidth = halfWidth * (1f - (i / (float)(_points.Count - 1)));

                Vector2 normal = CurveNormal(_points, i);
                Vector2 clockwise = _points[i] + normal * thisPointsWidth;
                Vector2 cclockwise = _points[i] - normal * thisPointsWidth;
                Color color = _trailColor.GetColourAt(currentDistance, trailLength, _points);

                AddVertex(clockwise, color, Vector2.UnitX * i);
                AddVertex(prevClockwise, previousColor, Vector2.UnitX * (i - 1));
                AddVertex(prevCClockwise, previousColor, new Vector2(i - 1, 1f));

                AddVertex(clockwise, color, Vector2.UnitX * i);
                AddVertex(prevCClockwise, previousColor, new Vector2(i - 1, 1f));
                AddVertex(cclockwise, color, new Vector2(i, 1f));

                prevClockwise = clockwise;
                prevCClockwise = cclockwise;
                previousColor = color;
            }

            //set effect parameter for matrix (todo: try have this only calculated when screen size changes?)
            int width = device.Viewport.Width;
            int height = device.Viewport.Height;
            Vector2 zoom = Main.GameViewMatrix.Zoom;
            Matrix view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up) * Matrix.CreateTranslation(width / 2, height / -2, 0) * Matrix.CreateRotationZ(MathHelper.Pi) * Matrix.CreateScale(zoom.X, zoom.Y, 1f);
            Matrix projection = Matrix.CreateOrthographic(width, height, 0, 1000);
            effect.Parameters["WorldViewProjection"].SetValue(view * projection);
            //effect.Parameters["WorldViewProjection"].SetValue(Main.GameViewMatrix.TransformationMatrix * Main.GameViewMatrix.ZoomMatrix);

            //apply this trail's shader pass and draw
            _trailShader.ApplyShader(effect, this, this._points);
            device.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, (_points.Count - 1) * 2 + _trailCap.ExtraTris);
        }

        //Helper methods
        private Vector2 CurveNormal(List<Vector2> points, int index)
        {
            if (points.Count == 1) return points[0];

            if (index == 0)
            {
                return Clockwise90(Vector2.Normalize(points[1] - points[0]));
            }
            if (index == points.Count - 1)
            {
                return Clockwise90(Vector2.Normalize(points[index] - points[index - 1]));
            }
            return Clockwise90(Vector2.Normalize(points[index + 1] - points[index - 1]));
        }

        private Vector2 Clockwise90(Vector2 vector)
        {
            return new Vector2(-vector.Y, vector.X);
        }
    }

    public interface ITrailShader
    {
        string ShaderPass { get; }
        void ApplyShader(Effect effect, Trail trail, List<Vector2> positions);
    }

    public class DefaultShader : ITrailShader
    {
        public string ShaderPass => "DefaultPass";
        public void ApplyShader(Effect effect, Trail trail, List<Vector2> positions)
        {
            effect.CurrentTechnique.Passes[ShaderPass].Apply();
        }
    }

    public class ImageShader : ITrailShader
    {
        public string ShaderPass => "BasicImagePass";

        protected Vector2 _coordMult;
        protected float _xOffset;
        protected float _yAnimSpeed;
        protected float _strength;
        private Texture2D _texture;

        public ImageShader(Texture2D image, Vector2 coordinateMultiplier, float strength = 1f, float yAnimSpeed = 0f)
        {
            _coordMult = coordinateMultiplier;
            _strength = strength;
            _yAnimSpeed = yAnimSpeed;
            _texture = image;
        }

        public ImageShader(Texture2D image, float xCoordinateMultiplier, float yCoordinateMultiplier, float strength = 1f, float yAnimSpeed = 0f) : this(image, new Vector2(xCoordinateMultiplier, yCoordinateMultiplier), strength, yAnimSpeed)
        {
        }

        public void ApplyShader(Effect effect, Trail trail, List<Vector2> positions)
        {
            _xOffset -= _coordMult.X;
            effect.Parameters["imageTexture"].SetValue(_texture);
            effect.Parameters["coordOffset"].SetValue(new Vector2(_xOffset, Main.GlobalTime * _yAnimSpeed));
            effect.Parameters["coordMultiplier"].SetValue(_coordMult);
            effect.Parameters["strength"].SetValue(_strength);
            effect.CurrentTechnique.Passes[ShaderPass].Apply();
        }
    }

    public interface ITrailPosition
    {
        Vector2 GetNextTrailPosition(Projectile projectile);
    }

    public class DefaultTrailPosition : ITrailPosition
    {
        public Vector2 GetNextTrailPosition(Projectile projectile)
        {
            return projectile.Center;
        }
    }

    public class SleepingStarTrailPosition : ITrailPosition
    {
        public Vector2 GetNextTrailPosition(Projectile projectile)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            return projectile.position + drawOrigin + Vector2.UnitY * projectile.gfxOffY;
        }
    }
    public interface ITrailColor
    {
        Color GetColourAt(float distanceFromStart, float trailLength, List<Vector2> points);
    }

    #region Different Trail Color Types
    public class GradientTrail : ITrailColor
    {
        private Color _startColour;
        private Color _endColour;

        public GradientTrail(Color start, Color end)
        {
            _startColour = start;
            _endColour = end;
        }

        public Color GetColourAt(float distanceFromStart, float trailLength, List<Vector2> points)
        {
            float progress = distanceFromStart / trailLength;
            return Color.Lerp(_startColour, _endColour, progress) * (1f - progress);
        }
    }

    public class RainbowTrail : ITrailColor
    {
        private float _saturation;
        private float _lightness;
        private float _speed;
        private float _distanceMultiplier;

        public RainbowTrail(float animationSpeed = 5f, float distanceMultiplier = 0.01f, float saturation = 1f, float lightness = 0.5f)
        {
            _saturation = saturation;
            _lightness = lightness;
            _distanceMultiplier = distanceMultiplier;
            _speed = animationSpeed;
        }

        public Color GetColourAt(float distanceFromStart, float trailLength, List<Vector2> points)
        {
            float progress = distanceFromStart / trailLength;
            float hue = (Main.GlobalTime * _speed + distanceFromStart * _distanceMultiplier) % MathHelper.TwoPi;
            return ColorFromHSL(hue, _saturation, _lightness) * (1f - progress);
        }

        //Borrowed methods for converting HSL to RGB
        private Color ColorFromHSL(float h, float s, float l)
        {
            h /= MathHelper.TwoPi;

            float r = 0, g = 0, b = 0;
            if (l != 0)
            {
                if (s == 0)
                    r = g = b = l;
                else
                {
                    float temp2;
                    if (l < 0.5f)
                        temp2 = l * (1f + s);
                    else
                        temp2 = l + s - (l * s);

                    float temp1 = 2f * l - temp2;

                    r = GetColorComponent(temp1, temp2, h + 0.33333333f);
                    g = GetColorComponent(temp1, temp2, h);
                    b = GetColorComponent(temp1, temp2, h - 0.33333333f);
                }
            }
            return new Color(r, g, b);
        }
        private float GetColorComponent(float temp1, float temp2, float temp3)
        {
            if (temp3 < 0f)
                temp3 += 1f;
            else if (temp3 > 1f)
                temp3 -= 1f;

            if (temp3 < 0.166666667f)
                return temp1 + (temp2 - temp1) * 6f * temp3;
            else if (temp3 < 0.5f)
                return temp2;
            else if (temp3 < 0.66666666f)
                return temp1 + ((temp2 - temp1) * (0.66666666f - temp3) * 6f);
            else
                return temp1;
        }
    }

    public class StandardColorTrail : ITrailColor
    {
        private Color _colour;

        public StandardColorTrail(Color colour)
        {
            _colour = colour;
        }

        public Color GetColourAt(float distanceFromStart, float trailLength, List<Vector2> points)
        {
            float progress = distanceFromStart / trailLength;
            return _colour * (1f - progress);
        }
    }
    #endregion

    public interface ITrailCap
    {
        int ExtraTris { get; }
        void AddCap(VertexPositionColorTexture[] array, ref int currentIndex, Color colour, Vector2 position, Vector2 startNormal, float width);
    }

    #region Different Trail Caps
    public class RoundCap : ITrailCap
    {
        public int ExtraTris => 20;

        public void AddCap(VertexPositionColorTexture[] array, ref int currentIndex, Color colour, Vector2 position, Vector2 startNormal, float width)
        {
            //initial info
            float halfWidth = width * 0.5f;
            float arcStart = startNormal.ToRotation();
            float arcAmount = MathHelper.Pi;
            //int segments = (int)Math.Ceiling(6 * Math.Sqrt(halfWidth) * (arcAmount / MathHelper.TwoPi));
            int segments = ExtraTris;
            float theta = arcAmount / segments;
            float cos = (float)Math.Cos(theta);
            float sin = (float)Math.Sin(theta);
            float t;
            float x = (float)Math.Cos(arcStart) * halfWidth;
            float y = (float)Math.Sin(arcStart) * halfWidth;

            position -= Main.screenPosition;

            //create initial vertices
            VertexPositionColorTexture center = new VertexPositionColorTexture(new Vector3(position.X, position.Y, 0f), colour, Vector2.One * 0.5f);
            VertexPositionColorTexture prev = new VertexPositionColorTexture(new Vector3(position.X + x, position.Y + y, 0f), colour, Vector2.One);

            for (int i = 0; i < segments; i++)
            {
                //apply matrix transformation
                t = x;
                x = cos * x - sin * y;
                y = sin * t + cos * y;

                VertexPositionColorTexture next = new VertexPositionColorTexture(new Vector3(position.X + x, position.Y + y, 0f), colour, Vector2.One);

                //Add triangle vertices
                array[currentIndex++] = center;
                array[currentIndex++] = prev;
                array[currentIndex++] = next;

                prev = next;
            }
        }
    }
    public class NoCap : ITrailCap
    {
        public int ExtraTris => 0;

        public void AddCap(VertexPositionColorTexture[] array, ref int currentIndex, Color colour, Vector2 position, Vector2 startNormal, float width)
        {

        }
    }
    #endregion
}
