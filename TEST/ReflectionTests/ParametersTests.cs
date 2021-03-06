﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLogic.Mapper;
using ApplicationLogic.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TEST
{
    [TestClass]
    public class ParametersTests
    {
        [TestInitialize]
        public void Initialize()
        {
            TestConstants.Context = new NodeItemMapper();
            TestStorageInitializers.AssemblyInitialize();
            TestStorageInitializers.NamespacesInitialize();
            TestStorageInitializers.TypesInitialize();
            TestStorageInitializers.PropertiesInitialize();
        }

        [TestMethod]
        public void Map_PropertiesInitialized_ShouldBeOk()
        {
            NodeItem actual = TestConstants.Context.Map(TestConstants.Storage);
            NodeItem expected = new NodeItem(TestConstants.AssemblyName, true)
            {
                Children =
                {
                    new NodeItem($"Namespace: {TestConstants.NamespaceName}", true)
                    {
                        Children =
                        {
                            new NodeItem($"Class: {TestConstants.TypeName}", true)
                            {
                                Children =
                                {
                                    new NodeItem($"Property: {TestConstants.PropertyName}", true)
                                    {
                                        Children =
                                        {
                                            new NodeItem($"Enum: {TestConstants.SecondTypeName}", false)
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}