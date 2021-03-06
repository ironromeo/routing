﻿// OsmSharp - OpenStreetMap (OSM) SDK
// Copyright (C) 2016 Abelshausen Ben
// 
// This file is part of Itinero.
// 
// OsmSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// OsmSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Itinero. If not, see <http://www.gnu.org/licenses/>.

using NUnit.Framework;
using Itinero.LocalGeo;
using Itinero.Attributes;
using System.Collections.Generic;

namespace Itinero.Test
{
    /// <summary>
    /// Contains tests for the route class and route extensions.
    /// </summary>
    [TestFixture]
    public class RouteTests
    {
        /// <summary>
        /// Tests route concatenation.
        /// </summary>
        [Test]
        public void TestConcatenate()
        {
            var route1 = new Route()
            {
                Shape = new Coordinate[]
                {
                    new Coordinate()
                    {
                        Latitude = 51.267819164340295f,
                        Longitude = 4.801352620124817f
                    },
                    new Coordinate()
                    {
                        Latitude = 51.26821857585588f,
                        Longitude = 4.801352620124817f
                    }
                },
                ShapeMeta = new Route.Meta[]
                {
                    new Route.Meta()
                    {
                        Shape = 0
                    },
                    new Route.Meta()
                    {
                        Shape = 1,
                        Distance = 100,
                        Time = 60
                    }
                },
                Stops = new Route.Stop[]
                {
                    new Route.Stop()
                    {
                        Shape = 0
                    },
                    new Route.Stop()
                    {
                        Shape = 1,
                        Distance = 100,
                        Time = 60
                    }
                },
                Attributes = new AttributeCollection(),
                TotalDistance = 100,
                TotalTime = 60
            };
            var route2 = new Route()
            {
                Shape = new Coordinate[]
                {
                    new Coordinate()
                    {
                        Latitude = 51.26821857585588f,
                        Longitude = 4.801352620124817f
                    },
                    new Coordinate()
                    {
                        Latitude = 51.267819164340295f,
                        Longitude = 4.801352620124817f
                    }
                },
                ShapeMeta = new Route.Meta[]
                {
                    new Route.Meta()
                    {
                        Shape = 0
                    },
                    new Route.Meta()
                    {
                        Shape = 1,
                        Distance = 100,
                        Time = 60
                    }
                },
                Stops = new Route.Stop[]
                {
                    new Route.Stop()
                    {
                        Shape = 0
                    },
                    new Route.Stop()
                    {
                        Shape = 1,
                        Distance = 100,
                        Time = 60
                    }
                },
                Attributes = new AttributeCollection(),
                TotalDistance = 100,
                TotalTime = 60
            };

            var route = route1.Concatenate(route2);

            Assert.IsNotNull(route);
            Assert.IsNotNull(route.Shape);
            Assert.AreEqual(3, route.Shape.Length);
            Assert.AreEqual(3, route.ShapeMeta.Length);
            Assert.AreEqual(0, route.ShapeMeta[0].Distance);
            Assert.AreEqual(0, route.ShapeMeta[0].Time);
            Assert.AreEqual(100, route.ShapeMeta[1].Distance);
            Assert.AreEqual(60, route.ShapeMeta[1].Time);
            Assert.AreEqual(200, route.ShapeMeta[2].Distance);
            Assert.AreEqual(120, route.ShapeMeta[2].Time);
            Assert.AreEqual(0, route.Stops[0].Distance);
            Assert.AreEqual(0, route.Stops[0].Time);
            Assert.AreEqual(100, route.Stops[1].Distance);
            Assert.AreEqual(60, route.Stops[1].Time);
            Assert.AreEqual(200, route.Stops[2].Distance);
            Assert.AreEqual(120, route.Stops[2].Time);
            Assert.AreEqual(200, route.TotalDistance);
            Assert.AreEqual(120, route.TotalTime);
        }

        /// <summary>
        /// Tests the route enumeration.
        /// </summary>
        [Test]
        public void TestEnumerator()
        {
            var route = new Route()
            {
                Shape = new Coordinate[]
                {
                    new Coordinate(0, 0),
                    new Coordinate(0, 0),
                    new Coordinate(0, 0)
                },
                ShapeMeta = new Route.Meta[]
                {
                    new Route.Meta()
                    {
                        Shape = 0
                    },
                    new Route.Meta()
                    {
                        Shape = 1
                    },
                    new Route.Meta()
                    {
                        Shape = 2
                    }
                },
                TotalDistance = 0,
                TotalTime = 0
            };

            var enumerator = route.GetEnumerator();
            var positions = new List<RoutePosition>();
            while(enumerator.MoveNext())
            {
                positions.Add(enumerator.Current);
            }

            Assert.AreEqual(3, positions.Count);
            var position = positions[0];
            Assert.AreEqual(-1, position.BranchIndex);
            Assert.AreEqual(0, position.MetaIndex);
            Assert.AreEqual(0, position.Shape);
            Assert.AreEqual(-1, position.StopIndex);
            Assert.IsFalse(position.HasBranches());
            Assert.IsFalse(position.HasStops());
            Assert.IsTrue(position.HasCurrentMeta());
            position = positions[1];
            Assert.AreEqual(-1, position.BranchIndex);
            Assert.AreEqual(1, position.MetaIndex);
            Assert.AreEqual(1, position.Shape);
            Assert.AreEqual(-1, position.StopIndex);
            Assert.IsFalse(position.HasBranches());
            Assert.IsFalse(position.HasStops());
            Assert.IsTrue(position.HasCurrentMeta());
            position = positions[2];
            Assert.AreEqual(-1, position.BranchIndex);
            Assert.AreEqual(2, position.MetaIndex);
            Assert.AreEqual(2, position.Shape);
            Assert.AreEqual(-1, position.StopIndex);
            Assert.IsFalse(position.HasBranches());
            Assert.IsFalse(position.HasStops());
            Assert.IsTrue(position.HasCurrentMeta());
            
            route = new Route()
            {
                Shape = new Coordinate[]
                {
                    new Coordinate(0, 0),
                    new Coordinate(0, 0),
                    new Coordinate(0, 0)
                },
                ShapeMeta = new Route.Meta[]
                {
                    new Route.Meta()
                    {
                        Shape = 0
                    },
                    new Route.Meta()
                    {
                        Shape = 2
                    }
                },
                TotalDistance = 0,
                TotalTime = 0
            };

            enumerator = route.GetEnumerator();
            positions = new List<RoutePosition>();
            while (enumerator.MoveNext())
            {
                positions.Add(enumerator.Current);
            }

            Assert.AreEqual(3, positions.Count);
            position = positions[0];
            Assert.AreEqual(-1, position.BranchIndex);
            Assert.AreEqual(0, position.MetaIndex);
            Assert.AreEqual(0, position.Shape);
            Assert.AreEqual(-1, position.StopIndex);
            Assert.IsFalse(position.HasBranches());
            Assert.IsFalse(position.HasStops());
            Assert.IsTrue(position.HasCurrentMeta());
            position = positions[1];
            Assert.AreEqual(-1, position.BranchIndex);
            Assert.AreEqual(1, position.MetaIndex);
            Assert.AreEqual(1, position.Shape);
            Assert.AreEqual(-1, position.StopIndex);
            Assert.IsFalse(position.HasBranches());
            Assert.IsFalse(position.HasStops());
            Assert.IsFalse(position.HasCurrentMeta());
            position = positions[2];
            Assert.AreEqual(-1, position.BranchIndex);
            Assert.AreEqual(1, position.MetaIndex);
            Assert.AreEqual(2, position.Shape);
            Assert.AreEqual(-1, position.StopIndex);
            Assert.IsFalse(position.HasBranches());
            Assert.IsFalse(position.HasStops());
            Assert.IsTrue(position.HasCurrentMeta());

            route = new Route()
            {
                Shape = new Coordinate[]
                {
                    new Coordinate(0, 0),
                    new Coordinate(0, 0),
                    new Coordinate(0, 0)
                },
                ShapeMeta = new Route.Meta[]
                {
                    new Route.Meta()
                    {
                        Shape = 0
                    },
                    new Route.Meta()
                    {
                        Shape = 2
                    }
                },
                Stops = new Route.Stop[]
                {
                    new Route.Stop()
                    {
                        Shape = 1
                    }
                },
                Branches = new Route.Branch[]
                {
                    new Route.Branch()
                    {
                        Shape = 1
                    }
                },
                TotalDistance = 0,
                TotalTime = 0
            };

            enumerator = route.GetEnumerator();
            positions = new List<RoutePosition>();
            while (enumerator.MoveNext())
            {
                positions.Add(enumerator.Current);
            }

            Assert.AreEqual(3, positions.Count);
            position = positions[0];
            Assert.AreEqual(0, position.BranchIndex);
            Assert.AreEqual(0, position.MetaIndex);
            Assert.AreEqual(0, position.Shape);
            Assert.AreEqual(0, position.StopIndex);
            Assert.IsFalse(position.HasBranches());
            Assert.IsFalse(position.HasStops());
            Assert.IsTrue(position.HasCurrentMeta());
            position = positions[1];
            Assert.AreEqual(0, position.BranchIndex);
            Assert.AreEqual(1, position.MetaIndex);
            Assert.AreEqual(1, position.Shape);
            Assert.AreEqual(0, position.StopIndex);
            Assert.IsTrue(position.HasBranches());
            Assert.IsTrue(position.HasStops());
            Assert.IsFalse(position.HasCurrentMeta());
            position = positions[2];
            Assert.AreEqual(1, position.BranchIndex);
            Assert.AreEqual(1, position.MetaIndex);
            Assert.AreEqual(2, position.Shape);
            Assert.AreEqual(1, position.StopIndex);
            Assert.IsFalse(position.HasBranches());
            Assert.IsFalse(position.HasStops());
            Assert.IsTrue(position.HasCurrentMeta());
        }

        /// <summary>
        /// Tests writing a route as json.
        /// </summary>
        [Test]
        public void TestWriteJson()
        {
            var route = new Route()
            {
                Shape = new Coordinate[]
                {
                    new Coordinate()
                    {
                        Latitude = 51.267819164340295f,
                        Longitude = 4.801352620124817f
                    },
                    new Coordinate()
                    {
                        Latitude = 51.26821857585588f,
                        Longitude = 4.801352620124817f
                    }
                },
                ShapeMeta = new Route.Meta[]
                {
                    new Route.Meta()
                    {
                        Shape = 0
                    },
                    new Route.Meta()
                    {
                        Shape = 1,
                        Distance = 100,
                        Time = 60,
                        Attributes = new AttributeCollection(
                            new Attribute("highway", "residential")),
                    }
                },
                Stops = new Route.Stop[]
                {
                    new Route.Stop()
                    {
                        Shape = 1,
                        Attributes = new AttributeCollection(
                            new Attribute("address", "Pastorijstraat 102, 2275 Wechelderzande")),
                        Coordinate = new Coordinate()
                        {
                            Latitude = 51.26821857585588f,
                            Longitude = 4.801352620124817f
                        }
                    }
                },
                Branches = new Route.Branch[]
                {
                    new Route.Branch()
                    {
                        Shape = 1,
                        Attributes = new AttributeCollection(
                            new Attribute("highway", "residential")),
                        Coordinate = new Coordinate()
                        {
                            Latitude = 51.26821857585588f,
                            Longitude = 4.801352620124817f
                        }
                    }
                },
                Attributes = new AttributeCollection(),
                TotalDistance = 100,
                TotalTime = 60
            };

            var json = route.ToJson();
            Assert.AreEqual("{\"Shape\":[[4.801353,51.26782],[4.801353,51.26822]],\"ShapeMeta\":[{\"Shape\":0},{\"Shape\":1,\"Attributes\":{\"highway\":\"residential\"}}],\"Stops\":[{\"Shape\":1,\"Coordinates\":[4.801353,51.26822],\"Attributes\":{\"address\":\"Pastorijstraat 102, 2275 Wechelderzande\"}}],\"Branches\":[{\"Shape\":1,\"Coordinates\":[4.801353,51.26822],\"Attributes\":{\"highway\":\"residential\"}}]}",
                json);
        }

        /// <summary>
        /// Tests writing a route as xml.
        /// </summary>
        [Test]
        public void TestWriteXml()
        {
            var route = new Route()
            {
                Shape = new Coordinate[]
                {
                    new Coordinate()
                    {
                        Latitude = 51.267819164340295f,
                        Longitude = 4.801352620124817f
                    },
                    new Coordinate()
                    {
                        Latitude = 51.26821857585588f,
                        Longitude = 4.801352620124817f
                    }
                },
                ShapeMeta = new Route.Meta[]
                {
                    new Route.Meta()
                    {
                        Shape = 0
                    },
                    new Route.Meta()
                    {
                        Shape = 1,
                        Distance = 100,
                        Time = 60,
                        Attributes = new AttributeCollection(
                            new Attribute("highway", "residential")),
                    }
                },
                Stops = new Route.Stop[]
                {
                    new Route.Stop()
                    {
                        Shape = 1,
                        Attributes = new AttributeCollection(
                            new Attribute("address", "Pastorijstraat 102, 2275 Wechelderzande")),
                        Coordinate = new Coordinate()
                        {
                            Latitude = 51.26821857585588f,
                            Longitude = 4.801352620124817f
                        }
                    }
                },
                Branches = new Route.Branch[]
                {
                    new Route.Branch()
                    {
                        Shape = 1,
                        Attributes = new AttributeCollection(
                            new Attribute("highway", "residential")),
                        Coordinate = new Coordinate()
                        {
                            Latitude = 51.26821857585588f,
                            Longitude = 4.801352620124817f
                        }
                    }
                },
                Attributes = new AttributeCollection(),
                TotalDistance = 100,
                TotalTime = 60
            };

            var xml = route.ToXml();
            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<route>\r\n  <shape>\r\n    <c lat=\"51.26782\" lon=\"4.801353\" />\r\n    <c lat=\"51.26822\" lon=\"4.801353\" />\r\n  </shape>\r\n  <metas>\r\n    <meta shape=\"0\" />\r\n    <meta shape=\"1\">\r\n      <property k=\"highway\" v=\"residential\" />\r\n    </meta>\r\n  </metas>\r\n  <branches>\r\n    <branch shape=\"1\">\r\n      <property k=\"highway\" v=\"residential\" />\r\n    </branch>\r\n  </branches>\r\n  <stops>\r\n    <stop shape=\"1\" lat=\"51.26822\" lon=\"4.801353\">\r\n      <property k=\"address\" v=\"Pastorijstraat 102, 2275 Wechelderzande\" />\r\n    </stop>\r\n  </stops>\r\n</route>",
                xml);
        }

        /// <summary>
        /// Tests writing a route as geojson.
        /// </summary>
        [Test]
        public void TestWriteGeoJson()
        {
            var route = new Route()
            {
                Shape = new Coordinate[]
                {
                    new Coordinate()
                    {
                        Latitude = 51.267819164340295f,
                        Longitude = 4.801352620124817f
                    },
                    new Coordinate()
                    {
                        Latitude = 51.26821857585588f,
                        Longitude = 4.801352620124817f
                    }
                },
                ShapeMeta = new Route.Meta[]
                {
                    new Route.Meta()
                    {
                        Shape = 0
                    },
                    new Route.Meta()
                    {
                        Shape = 1,
                        Distance = 100,
                        Time = 60,
                        Attributes = new AttributeCollection(
                            new Attribute("highway", "residential")),
                    }
                },
                Stops = new Route.Stop[]
                {
                    new Route.Stop()
                    {
                        Shape = 1,
                        Attributes = new AttributeCollection(
                            new Attribute("address", "Pastorijstraat 102, 2275 Wechelderzande")),
                        Coordinate = new Coordinate()
                        {
                            Latitude = 51.26821857585588f,
                            Longitude = 4.801352620124817f
                        }
                    }
                },
                Branches = new Route.Branch[]
                {
                    new Route.Branch()
                    {
                        Shape = 1,
                        Attributes = new AttributeCollection(
                            new Attribute("highway", "residential")),
                        Coordinate = new Coordinate()
                        {
                            Latitude = 51.26821857585588f,
                            Longitude = 4.801352620124817f
                        }
                    }
                },
                Attributes = new AttributeCollection(),
                TotalDistance = 100,
                TotalTime = 60
            };

            var geojson = route.ToGeoJson();
        }

        /// <summary>
        /// Tests route concatenation with identical stops.
        /// </summary>
        [Test]
        public void TestConcatenateWithIdenticalStops()
        {
            var route1 = new Route()
            {
                Shape = new Coordinate[]
                {
                    new Coordinate()
                    {
                        Latitude = 51.267819164340295f,
                        Longitude = 4.801352620124817f
                    },
                    new Coordinate()
                    {
                        Latitude = 51.26821857585588f,
                        Longitude = 4.801352620124817f
                    }
                },
                ShapeMeta = new Route.Meta[]
                {
                    new Route.Meta()
                    {
                        Shape = 0
                    },
                    new Route.Meta()
                    {
                        Shape = 1,
                        Distance = 100,
                        Time = 60
                    }
                },
                Stops = new Route.Stop[]
                {
                    new Route.Stop()
                    {
                        Attributes = null,
                        Coordinate = new Coordinate(0, 0),
                        Shape = 1
                    }
                },
                Attributes = new AttributeCollection(),
                TotalDistance = 100,
                TotalTime = 60
            };
            var route2 = new Route()
            {
                Shape = new Coordinate[]
                {
                    new Coordinate()
                    {
                        Latitude = 51.26821857585588f,
                        Longitude = 4.801352620124817f
                    },
                    new Coordinate()
                    {
                        Latitude = 51.267819164340295f,
                        Longitude = 4.801352620124817f
                    }
                },
                ShapeMeta = new Route.Meta[]
                {
                    new Route.Meta()
                    {
                        Shape = 0
                    },
                    new Route.Meta()
                    {
                        Shape = 1,
                        Distance = 100,
                        Time = 60
                    }
                },
                Stops = new Route.Stop[]
                {
                    new Route.Stop()
                    {
                        Attributes = null,
                        Coordinate = new Coordinate(0, 0),
                        Shape = 0
                    }
                },
                Attributes = new AttributeCollection(),
                TotalDistance = 100,
                TotalTime = 60
            };

            var route = route1.Concatenate(route2);

            Assert.IsNotNull(route);
            Assert.IsNotNull(route.Shape);
            Assert.AreEqual(3, route.Shape.Length);
            Assert.AreEqual(3, route.ShapeMeta.Length);
            Assert.AreEqual(0, route.ShapeMeta[0].Distance);
            Assert.AreEqual(0, route.ShapeMeta[0].Time);
            Assert.AreEqual(100, route.ShapeMeta[1].Distance);
            Assert.AreEqual(60, route.ShapeMeta[1].Time);
            Assert.AreEqual(200, route.ShapeMeta[2].Distance);
            Assert.AreEqual(120, route.ShapeMeta[2].Time);
            Assert.IsNotNull(route.Stops);
            Assert.AreEqual(1, route.Stops.Length);
            Assert.AreEqual(200, route.TotalDistance);
            Assert.AreEqual(120, route.TotalTime);
        }

        /// <summary>
        /// Test getting a segment.
        /// </summary>
        [Test]
        public void TestGetSegment()
        {
            var route = new Route()
            {
                Shape = new Coordinate[]
                {
                    new Coordinate()
                    {
                        Latitude = 51.267819164340295f,
                        Longitude = 4.801352620124817f
                    },
                    new Coordinate()
                    {
                        Latitude = 51.26821857585588f,
                        Longitude = 4.801352620124817f
                    },
                    new Coordinate()
                    {
                        Latitude = 51.26821857585588f,
                        Longitude = 4.801352620124817f
                    },
                    new Coordinate()
                    {
                        Latitude = 51.26821857585588f,
                        Longitude = 4.801352620124817f
                    },
                    new Coordinate()
                    {
                        Latitude = 51.26821857585588f,
                        Longitude = 4.801352620124817f
                    }
                },
                ShapeMeta = new Route.Meta[]
                {
                    new Route.Meta()
                    {
                        Shape = 0
                    },
                    new Route.Meta()
                    {
                        Shape = 2,
                        Distance = 100,
                        Time = 60
                    },
                    new Route.Meta()
                    {
                        Shape = 4,
                        Distance = 200,
                        Time = 120
                    }
                },
                Attributes = new AttributeCollection(),
                TotalDistance = 100,
                TotalTime = 60
            };

            int segmentStart, segmentEnd;
            route.SegmentFor(0, out segmentStart, out segmentEnd);
            Assert.AreEqual(0, segmentStart);
            Assert.AreEqual(2, segmentEnd);
            route.SegmentFor(1, out segmentStart, out segmentEnd);
            Assert.AreEqual(0, segmentStart);
            Assert.AreEqual(2, segmentEnd);
            route.SegmentFor(2, out segmentStart, out segmentEnd);
            Assert.AreEqual(2, segmentStart);
            Assert.AreEqual(4, segmentEnd);
            route.SegmentFor(3, out segmentStart, out segmentEnd);
            Assert.AreEqual(2, segmentStart);
            Assert.AreEqual(4, segmentEnd);
            route.SegmentFor(4, out segmentStart, out segmentEnd);
            Assert.AreEqual(2, segmentStart);
            Assert.AreEqual(4, segmentEnd);
        }

        /// <summary>
        /// Test distance and time at a shape.
        /// </summary>
        [Test]
        public void TestGetDistanceAndTimeAt()
        {
            var route = new Route()
            {
                Shape = new Coordinate[]
                {
                    new Coordinate()
                    {
                        Latitude = 49.76851543353109f,
                        Longitude = 5.912189483642578f
                    },
                    new Coordinate()
                    {
                        Latitude = 49.768522363042294f,
                        Longitude = 5.9135788679122925f
                    },
                    new Coordinate()
                    {
                        Latitude = 49.76852929255253f,
                        Longitude = 5.914962887763977f
                    },
                    new Coordinate()
                    {
                        Latitude = 49.76852929255253f,
                        Longitude = 5.916352272033691f
                    },
                    new Coordinate()
                    {
                        Latitude = 49.768546616323725f,
                        Longitude = 5.917741656303405f
                    }
                },
                ShapeMeta = new Route.Meta[]
                {
                    new Route.Meta()
                    {
                        Shape = 0
                    },
                    new Route.Meta()
                    {
                        Shape = 2,
                        Distance = 100,
                        Time = 60
                    },
                    new Route.Meta()
                    {
                        Shape = 4,
                        Distance = 200,
                        Time = 120
                    }
                },
                Attributes = new AttributeCollection(),
                TotalDistance = 200,
                TotalTime = 120
            };

            float time, distance;
            route.DistanceAndTimeAt(0, out distance, out time);
            Assert.AreEqual(0, distance);
            Assert.AreEqual(0, time);
            route.DistanceAndTimeAt(1, out distance, out time);
            Assert.AreEqual(50, distance, 1);
            Assert.AreEqual(30, time, 1);
            route.DistanceAndTimeAt(2, out distance, out time);
            Assert.AreEqual(100, distance, 1);
            Assert.AreEqual(60, time, 1);
            route.DistanceAndTimeAt(3, out distance, out time);
            Assert.AreEqual(150, distance, 1);
            Assert.AreEqual(90, time, 1);
            route.DistanceAndTimeAt(4, out distance, out time);
            Assert.AreEqual(200, distance, 1);
            Assert.AreEqual(120, time, 1);
        }
    }
}