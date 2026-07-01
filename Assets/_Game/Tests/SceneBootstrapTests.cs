using NUnit.Framework;
using UnityEngine;

namespace ThiefSignal.Tests
{
    [TestFixture]
    public class SceneBootstrapTests
    {
        private const float Tolerance = 0.0001f;

        [Test]
        public void BuildWaypoints_ReturnsFivePointsForValidHouseSize()
        {
            Vector2[] waypoints = SceneBootstrap.BuildWaypoints(new Vector2(6f, 4f));

            Assert.AreEqual(5, waypoints.Length);
        }

        [Test]
        public void BuildWaypoints_CenterPointIsOrigin()
        {
            Vector2[] waypoints = SceneBootstrap.BuildWaypoints(new Vector2(6f, 4f));

            Assert.AreEqual(Vector2.zero, waypoints[1]);
        }

        [Test]
        public void BuildWaypoints_FirstPointIsOutsideLeftAtHouseMidHeight()
        {
            Vector2[] waypoints = SceneBootstrap.BuildWaypoints(new Vector2(6f, 4f));

            Assert.AreEqual(-6f, waypoints[0].x, Tolerance);
            Assert.AreEqual(0f, waypoints[0].y, Tolerance);
        }

        [Test]
        public void BuildWaypoints_HorizontalPathIsSymmetric()
        {
            Vector2[] waypoints = SceneBootstrap.BuildWaypoints(new Vector2(6f, 4f));

            Assert.AreEqual(waypoints[0].x, -waypoints[2].x, Tolerance);
            Assert.AreEqual(waypoints[3].x, -waypoints[4].x, Tolerance);
        }

        [Test]
        public void BuildWaypoints_BottomPointsAreBelowHouse()
        {
            Vector2 houseSize = new Vector2(6f, 4f);

            Vector2[] waypoints = SceneBootstrap.BuildWaypoints(houseSize);

            Assert.AreEqual(waypoints[3].y, waypoints[4].y, Tolerance);
            Assert.Less(waypoints[3].y, -houseSize.y);
        }

        [Test]
        public void BuildWaypoints_ScalesWithHouseSize()
        {
            Vector2[] small = SceneBootstrap.BuildWaypoints(new Vector2(2f, 2f));
            Vector2[] large = SceneBootstrap.BuildWaypoints(new Vector2(10f, 6f));

            Assert.Less(large[0].x, small[0].x);
        }
    }
}
