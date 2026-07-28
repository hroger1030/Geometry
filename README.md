# Geometry

A lightweight 2D/3D/nD geometry library written in C# for games and other applications that don't need
high-precision math. It favors speed and simplicity over precision — values are stored as `float`, so this
is **not** a good fit for serious scientific or CAD-grade math. It is, however, fast, easy to use, and easy
to extend with new shapes.

Note that some objects are assumed to be grid-aligned (e.g. `Rectangle`, `Cube`, `AABB`). Making these
fully general (arbitrary rotation, etc.) is potential future work.

Test coverage is an ongoing effort. If you find a bug, please open an issue and it will be looked at as
soon as possible.

## Solution layout

| Project | Description |
|---|---|
| [GeometryLib](GeometryLib) | The library itself. Namespace: `Geometry`. |
| [GeometryTests](GeometryTests) | NUnit test suite for the library, mirroring the `Objects` folder structure. |

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

## License

MIT — see [LICENSE.txt](LICENSE.txt).
