using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.TestInfrastructure
{
    [TestClass]
    public abstract class BaseGraphicsTest : BaseTest
    {
        protected GraphicsDevice GraphicsDevice;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            // This only works with SharpDX (i.e. compiling with WindowsDX)
            GraphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach,
                new PresentationParameters());
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();

            GraphicsDevice.Dispose();
        }
    }
}
