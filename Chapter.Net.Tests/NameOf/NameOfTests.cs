// -----------------------------------------------------------------------------------------------------------------
// <copyright file="NameOfTests.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using DemoNamespace1;
using DemoNamespace1.DemoNamespaceSub1;
using DemoNamespace1.DemoNamespaceSub1.DemoNamespaceSubSub1;
using DemoNamespace2;
using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class NameOfTests
{
    [Test]
    public void NameT_CalledWithoutPropertyName_ReturnsOnlyTypeName()
    {
        var result = NameOf.Name<TypeOneSub>();

        Assert.That(result, Is.EqualTo("TypeOneSub"));
    }

    [Test]
    public void NameT_CalledWithNullPropertyName_ReturnsOnlyTypeName()
    {
        var result = NameOf.Name<TypeOneSub>();

        Assert.That(result, Is.EqualTo("TypeOneSub"));
    }

    [Test]
    public void NameT_CalledWithEmptyPropertyName_ReturnsOnlyTypeName()
    {
        var result = NameOf.Name<TypeOneSub>(string.Empty);

        Assert.That(result, Is.EqualTo("TypeOneSub"));
    }

    [Test]
    public void NameT_CalledWithWhitespacePropertyName_ReturnsOnlyTypeName()
    {
        var result = NameOf.Name<TypeOneSub>("  ");

        Assert.That(result, Is.EqualTo("TypeOneSub"));
    }

    [Test]
    public void NameT_CalledWithPropertyName_ReturnsOnlyTypeNameAndPropertyNameConcatenated()
    {
        var result = NameOf.Name<TypeOneSub>("PropertyName");

        Assert.That(result, Is.EqualTo("TypeOneSub.PropertyName"));
    }

    [Test]
    public void Name_CalledWithNullType_ThrowsException()
    {
        Type type = null;

        Assert.That(() => NameOf.Name(type), Throws.ArgumentNullException);
    }

    [Test]
    public void Name_CalledWithoutPropertyName_ReturnsOnlyTypeName()
    {
        var result = NameOf.Name(typeof(TypeOneSub));

        Assert.That(result, Is.EqualTo("TypeOneSub"));
    }

    [Test]
    public void Name_CalledWithNullPropertyName_ReturnsOnlyTypeName()
    {
        var result = NameOf.Name(typeof(TypeOneSub));

        Assert.That(result, Is.EqualTo("TypeOneSub"));
    }

    [Test]
    public void Name_CalledWithEmptyPropertyName_ReturnsOnlyTypeName()
    {
        var result = NameOf.Name(typeof(TypeOneSub), string.Empty);

        Assert.That(result, Is.EqualTo("TypeOneSub"));
    }

    [Test]
    public void Name_CalledWithWhitespacePropertyName_ReturnsOnlyTypeName()
    {
        var result = NameOf.Name(typeof(TypeOneSub), "  ");

        Assert.That(result, Is.EqualTo("TypeOneSub"));
    }

    [Test]
    public void Name_CalledWithPropertyName_ReturnsOnlyTypeNameAndPropertyNameConcatenated()
    {
        var result = NameOf.Name(typeof(TypeOneSub), "PropertyName");

        Assert.That(result, Is.EqualTo("TypeOneSub.PropertyName"));
    }

    [Test]
    public void NamespaceT_Called_ReturnsTypeNamespace()
    {
        var result = NameOf.Namespace<TypeOneSub>();

        Assert.That(result, Is.EqualTo("DemoNamespace1.DemoNamespaceSub1"));
    }

    [Test]
    public void Namespace_CalledWithNullType_ThrowsException()
    {
        Type type = null;

        Assert.That(() => NameOf.Namespace(type), Throws.ArgumentNullException);
    }

    [Test]
    public void Namespace_Called_ReturnsTypeNamespace()
    {
        var result = NameOf.Namespace(typeof(TypeOneSub));

        Assert.That(result, Is.EqualTo("DemoNamespace1.DemoNamespaceSub1"));
    }

    [Test]
    public void NamespaceT2_CalledType1BelowType2_ReturnsRelativeNamespace()
    {
        var result = NameOf.Namespace<TypeOneA, TypeOneSubSub>();

        Assert.That(result, Is.EqualTo("DemoNamespaceSub1.DemoNamespaceSubSub1"));
    }

    [Test]
    public void NamespaceT2_CalledType2BelowType1_ReturnsRelativeNamespace()
    {
        var result = NameOf.Namespace<TypeOneSubSub, TypeOneA>();

        Assert.That(result, Is.EqualTo("DemoNamespaceSub1.DemoNamespaceSubSub1"));
    }

    [Test]
    public void NamespaceT2_CalledWithBothOnSameNamespace_ReturnsEmptyString()
    {
        var result = NameOf.Namespace<TypeOneA, TypeOneB>();

        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void NamespaceT2_CalledWithNameSpacesNotInSameRoot_ReturnsEmptyString()
    {
        var result = NameOf.Namespace<TypeOneA, TypeTwo>();

        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Namespace2_CalledWithNullType1_ThrowsException()
    {
        var type1 = typeof(TypeOneA);
        Type type2 = null;

        Assert.That(() => NameOf.Namespace(type1, type2), Throws.ArgumentNullException);
    }

    [Test]
    public void Namespace2_CalledWithNullType2_ThrowsException()
    {
        Type type1 = null;
        var type2 = typeof(TypeOneA);

        Assert.That(() => NameOf.Namespace(type1, type2), Throws.ArgumentNullException);
    }

    [Test]
    public void Namespace2_CalledType1BelowType2_ReturnsRelativeNamespace()
    {
        var result = NameOf.Namespace(typeof(TypeOneA), typeof(TypeOneSubSub));

        Assert.That(result, Is.EqualTo("DemoNamespaceSub1.DemoNamespaceSubSub1"));
    }

    [Test]
    public void Namespace2_CalledType2BelowType1_ReturnsRelativeNamespace()
    {
        var result = NameOf.Namespace(typeof(TypeOneSubSub), typeof(TypeOneA));

        Assert.That(result, Is.EqualTo("DemoNamespaceSub1.DemoNamespaceSubSub1"));
    }

    [Test]
    public void Namespace2_CalledWithBothOnSameNamespace_ReturnsEmptyString()
    {
        var result = NameOf.Namespace(typeof(TypeOneA), typeof(TypeOneB));

        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Namespace2_CalledWithNameSpacesNotInSameRoot_ReturnsEmptyString()
    {
        var result = NameOf.Namespace(typeof(TypeOneA), typeof(TypeTwo));

        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void FullNameT_CalledWithoutPropertyName_ReturnsOnlyTypeFullName()
    {
        var result = NameOf.FullName<TypeOneSub>();

        Assert.That(result, Is.EqualTo("DemoNamespace1.DemoNamespaceSub1.TypeOneSub"));
    }

    [Test]
    public void FullNameT_CalledWithNullPropertyName_ReturnsOnlyTypeFullName()
    {
        var result = NameOf.FullName<TypeOneSub>();

        Assert.That(result, Is.EqualTo("DemoNamespace1.DemoNamespaceSub1.TypeOneSub"));
    }

    [Test]
    public void FullNameT_CalledWithEmptyPropertyName_ReturnsOnlyTypeFullName()
    {
        var result = NameOf.FullName<TypeOneSub>(string.Empty);

        Assert.That(result, Is.EqualTo("DemoNamespace1.DemoNamespaceSub1.TypeOneSub"));
    }

    [Test]
    public void FullNameT_CalledWithWhitespacePropertyName_ReturnsOnlyTypeFullName()
    {
        var result = NameOf.FullName<TypeOneSub>("  ");

        Assert.That(result, Is.EqualTo("DemoNamespace1.DemoNamespaceSub1.TypeOneSub"));
    }

    [Test]
    public void FullNameT_CalledWithPropertyName_ReturnsOnlyTypeFullNameAndPropertyNameConcatenated()
    {
        var result = NameOf.FullName<TypeOneSub>("PropertyName");

        Assert.That(result, Is.EqualTo("DemoNamespace1.DemoNamespaceSub1.TypeOneSub.PropertyName"));
    }

    [Test]
    public void FullName_CalledWithNullType_ThrowsException()
    {
        Type type = null;

        Assert.That(() => NameOf.FullName(type), Throws.ArgumentNullException);
    }

    [Test]
    public void FullName_CalledWithoutPropertyName_ReturnsOnlyTypeFullName()
    {
        var result = NameOf.FullName(typeof(TypeOneSub));

        Assert.That(result, Is.EqualTo("DemoNamespace1.DemoNamespaceSub1.TypeOneSub"));
    }

    [Test]
    public void FullName_CalledWithNullPropertyName_ReturnsOnlyTypeFullName()
    {
        var result = NameOf.FullName(typeof(TypeOneSub));

        Assert.That(result, Is.EqualTo("DemoNamespace1.DemoNamespaceSub1.TypeOneSub"));
    }

    [Test]
    public void FullName_CalledWithEmptyPropertyName_ReturnsOnlyTypeFullName()
    {
        var result = NameOf.FullName(typeof(TypeOneSub), string.Empty);

        Assert.That(result, Is.EqualTo("DemoNamespace1.DemoNamespaceSub1.TypeOneSub"));
    }

    [Test]
    public void FullName_CalledWithWhitespacePropertyName_ReturnsOnlyTypeFullName()
    {
        var result = NameOf.FullName(typeof(TypeOneSub), "  ");

        Assert.That(result, Is.EqualTo("DemoNamespace1.DemoNamespaceSub1.TypeOneSub"));
    }

    [Test]
    public void FullName_CalledWithPropertyName_ReturnsOnlyTypeFullNameAndPropertyNameConcatenated()
    {
        var result = NameOf.FullName(typeof(TypeOneSub), "PropertyName");

        Assert.That(result, Is.EqualTo("DemoNamespace1.DemoNamespaceSub1.TypeOneSub.PropertyName"));
    }
}