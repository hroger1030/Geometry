# Geometry

A lightweight 2D/3D/nD geometry library written in C# for games and other applications that don't need
high-precision math. It favors speed and simplicity over precision — values are stored as `float`, so this
is **not** a good fit for serious scientific or CAD-grade math. It is, however, fast, easy to use, and easy
to extend with new shapes.

Note that some objects are assumed to be grid-aligned (e.g. `Rectangle`, `Cube`, `AABB`). Making these
fully general (arbitrary rotation, etc.) is potential future work.

Test coverage is an ongoing effort. If you find a bug, please open an issue and it will be looked at as
soon as possible.

## Table of contents

- [Solution layout](#solution-layout)
- [File tree](#file-tree)
- [Requirements](#requirements)
- [Building and testing](#building-and-testing)
- [Objects](#objects)
- [Code examples](#code-examples)
  - [2D: points, vectors, and circles](#2d-points-vectors-and-circles)
  - [3D: bounding volumes](#3d-bounding-volumes)
  - [nD: arbitrary-dimension vectors](#nd-arbitrary-dimension-vectors)
- [License](#license)

## Solution layout

| Project | Description |
|---|---|
| [GeometryLib](GeometryLib) | The library itself. Namespace: `Geometry`. |
| [GeometryTests](GeometryTests) | NUnit test suite for the library, mirroring the `Objects` folder structure. |

## File tree

Only the files that matter for using or extending the library are listed below; build output
(`bin`/`obj`) and IDE folders are omitted.

```
Geometry/
├── Geometry.sln
├── LICENSE.txt
├── CLAUDE.md
├── GeometryLib/                    # Library project (namespace: Geometry)
│   ├── Geometry.csproj
│   ├── Interfaces/
│   │   ├── I1d.cs
│   │   ├── I2d.cs
│   │   └── I3d.cs
│   └── Objects/
│       ├── Constants.cs
│       ├── 2d/
│       │   ├── Point2.cs
│       │   ├── Vector2.cs
│       │   ├── Line2.cs
│       │   ├── Circle.cs
│       │   ├── Ellipse.cs
│       │   ├── Triangle2.cs
│       │   ├── Rectangle.cs
│       │   └── Polygon.cs
│       ├── 3d/
│       │   ├── Point3.cs
│       │   ├── Vector3.cs
│       │   ├── Ray.cs
│       │   ├── Plane3.cs
│       │   ├── Triangle3.cs
│       │   ├── Sphere.cs
│       │   ├── Cube.cs
│       │   ├── AABB.cs
│       │   └── Capsule.cs
│       └── Nd/
│           └── VectorN.cs
└── GeometryTests/                  # NUnit test project (namespace: GeometryTests)
    ├── GeometryTests.csproj
    └── Objects/                    # Mirrors GeometryLib/Objects/
        ├── 2d/
        ├── 3d/
        └── Nd/
```

## Requirements

- .NET 10 SDK
- Windows (target platform)

## Building and testing

```
dotnet build
dotnet test
```

## Objects

### 2D (`GeometryLib/Objects/2d`)

- Point2
- Vector2
- Line2
- Circle
- Ellipse
- Triangle2
- Rectangle
- Polygon

### 3D (`GeometryLib/Objects/3d`)

- Point3
- Vector3
- Ray
- Plane3
- Triangle3
- Sphere
- Cube
- AABB
- Capsule

### Higher dimension (`GeometryLib/Objects/Nd`)

- VectorN

### Interfaces (`GeometryLib/Interfaces`)

- I1d
- I2d
- I3d

## Code examples

### 2D: points, vectors, and circles

```csharp
using Geometry;

// Points and vectors
var start = new Point2(0f, 0f);
var end = new Point2(3f, 4f);
float distance = start.DistanceTo(end); // 5

var direction = new Vector2(end) - new Vector2(start);
direction.Normalize();

Point2 moved = start + (direction * 2f); // move 2 units toward `end`

// Circles: overlap and containment checks
var a = new Circle(x: 0f, y: 0f, radius: 5f);
var b = new Circle(x: 6f, y: 0f, radius: 2f);

bool overlapping = a.Intersects(b);   // true, circles touch/overlap
bool inside = a.Contains(new Point2(1f, 1f)); // true

float area = a.Area;
float circumference = a.Circumference;
```

### 3D: bounding volumes

```csharp
using Geometry;

var sphere = new Sphere(new Point3(0f, 0f, 0f), radius: 5f);
bool hit = sphere.Contains(new Point3(1f, 2f, 3f));

var box = new AABB(
    min: new Point3(-1f, -1f, -1f),
    max: new Point3(1f, 1f, 1f));

var other = new AABB(
    min: new Point3(0.5f, 0.5f, 0.5f),
    max: new Point3(2f, 2f, 2f));

bool boxesOverlap = box.Intersects(other);
float volume = box.Volume;
```

### nD: arbitrary-dimension vectors

```csharp
using Geometry;

var v1 = new VectorN(4);
v1.Axis[0] = 1f;
v1.Axis[1] = 2f;
v1.Axis[2] = 3f;
v1.Axis[3] = 4f;

var v2 = new VectorN(4);
v2.Axis[0] = 4f;
v2.Axis[1] = 3f;
v2.Axis[2] = 2f;
v2.Axis[3] = 1f;

VectorN sum = v1 + v2;
VectorN scaled = v1 * 2f;
```

## License

This project is licensed under the [MIT License](LICENSE.txt).

In short: you can use, copy, modify, merge, publish, distribute, sublicense, and sell copies of this
software, in both personal and commercial projects, with no obligation to open-source your own code.
The only requirement is that the original copyright notice and license text are kept with any
substantial portion of the software you redistribute. The software is provided "as is," without
warranty of any kind — the authors are not liable for any claim or damages arising from its use.

See [LICENSE.txt](LICENSE.txt) for the full, legally-binding text.
