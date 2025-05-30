// Copyright (c) 2023 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System;
using System.Collections.Generic;

// TODO: use System.MathF
using Mathf = UnityEngine.Mathf;

namespace Mediapipe.Tasks.Components.Containers
{
  public readonly struct Landmark : IEquatable<Landmark>
  {
    private const float _LandmarkTolerance = 1e-6f;

    public readonly float x;
    public readonly float y;
    public readonly float z;
    public readonly float? visibility;
    public readonly float? presence;
    public readonly string name;

    internal Landmark(float x, float y, float z, float? visibility, float? presence) : this(x, y, z, visibility, presence, null) {}

    internal Landmark(float x, float y, float z, float? visibility, float? presence, string name)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.visibility = visibility;
      this.presence = presence;
      this.name = name;
    }

    internal Landmark(NativeLandmark nativeLandmark) : this(
      nativeLandmark.x, nativeLandmark.y, nativeLandmark.z,
#pragma warning disable IDE0004
      nativeLandmark.hasVisibility ? (float?)nativeLandmark.visibility : null,
      nativeLandmark.hasPresence ? (float?)nativeLandmark.presence : null,
#pragma warning restore IDE0004
      nativeLandmark.name
    ) {}

#nullable enable
    public override bool Equals(object? obj) => obj is Landmark other && Equals(other);
#nullable disable

    bool IEquatable<Landmark>.Equals(Landmark other)
    {
      return Mathf.Abs(x - other.x) < _LandmarkTolerance &&
             Mathf.Abs(y - other.y) < _LandmarkTolerance &&
             Mathf.Abs(z - other.z) < _LandmarkTolerance;
    }

    public override int GetHashCode() => Tuple.Create(x, y, z).GetHashCode();

    public static bool operator ==(in Landmark lhs, in Landmark rhs) => lhs.Equals(rhs);
    public static bool operator !=(in Landmark lhs, in Landmark rhs) => !(lhs == rhs);

    public static Landmark CreateFrom(Mediapipe.Landmark proto)
    {
      return new Landmark(
        proto.X, proto.Y, proto.Z,
#pragma warning disable IDE0004
        proto.HasVisibility ? (float?)proto.Visibility : null,
        proto.HasPresence ? (float?)proto.Presence : null
#pragma warning restore IDE0004
      );
    }

    public override string ToString()
      => $"{{ \"x\": {x}, \"y\": {y}, \"z\": {z}, \"visibility\": {Util.Format(visibility)}, \"presence\": {Util.Format(presence)}, \"name\": \"{name}\" }}";
  }

  public readonly struct NormalizedLandmark : IEquatable<NormalizedLandmark>
  {
    private const float _LandmarkTolerance = 1e-6f;

    public readonly float x;
    public readonly float y;
    public readonly float z;
    public readonly float? visibility;
    public readonly float? presence;
    public readonly string name;

    public NormalizedLandmark(float x, float y, float z, float? visibility, float? presence) : this(x, y, z, visibility, presence, null) {}

    internal NormalizedLandmark(float x, float y, float z, float? visibility, float? presence, string name)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.visibility = visibility;
      this.presence = presence;
      this.name = name;
    }

    internal NormalizedLandmark(NativeNormalizedLandmark nativeLandmark) : this(
      nativeLandmark.x, nativeLandmark.y, nativeLandmark.z,
#pragma warning disable IDE0004
      nativeLandmark.hasVisibility ? (float?)nativeLandmark.visibility : null,
      nativeLandmark.hasPresence ? (float?)nativeLandmark.presence : null,
#pragma warning restore IDE0004
      nativeLandmark.name
    ) {}

#nullable enable
    public override bool Equals(object? obj) => obj is NormalizedLandmark other && Equals(other);
#nullable disable

    bool IEquatable<NormalizedLandmark>.Equals(NormalizedLandmark other)
    {
      return Mathf.Abs(x - other.x) < _LandmarkTolerance &&
             Mathf.Abs(y - other.y) < _LandmarkTolerance &&
             Mathf.Abs(z - other.z) < _LandmarkTolerance;
    }

    public override int GetHashCode() => Tuple.Create(x, y, z).GetHashCode();

    public static bool operator ==(in NormalizedLandmark lhs, in NormalizedLandmark rhs) => lhs.Equals(rhs);
    public static bool operator !=(in NormalizedLandmark lhs, in NormalizedLandmark rhs) => !(lhs == rhs);

    public static NormalizedLandmark CreateFrom(Mediapipe.NormalizedLandmark proto)
    {
      return new NormalizedLandmark(
        proto.X, proto.Y, proto.Z,
#pragma warning disable IDE0004
        proto.HasVisibility ? (float?)proto.Visibility : null,
        proto.HasPresence ? (float?)proto.Presence : null
#pragma warning restore IDE0004
      );
    }

    public override string ToString()
      => $"{{ \"x\": {x}, \"y\": {y}, \"z\": {z}, \"visibility\": {Util.Format(visibility)}, \"presence\": {Util.Format(presence)}, \"name\": \"{name}\" }}";
  }

  public readonly struct Landmarks
  {
    public readonly List<Landmark> landmarks;

    internal Landmarks(List<Landmark> landmarks)
    {
      this.landmarks = landmarks;
    }

    public static Landmarks Alloc(int capacity) => new Landmarks(new List<Landmark>(capacity));

    public static Landmarks CreateFrom(LandmarkList proto)
    {
      var result = default(Landmarks);
      Copy(proto, ref result);
      return result;
    }

    public static void Copy(LandmarkList source, ref Landmarks destination)
    {
      var landmarks = destination.landmarks ?? new List<Landmark>(source.Landmark.Count);
      landmarks.Clear();
      for (var i = 0; i < source.Landmark.Count; i++)
      {
        landmarks.Add(Landmark.CreateFrom(source.Landmark[i]));
      }

      destination = new Landmarks(landmarks);
    }

    internal static void Copy(NativeLandmarks source, ref Landmarks destination)
    {
      var landmarks = destination.landmarks ?? new List<Landmark>((int)source.landmarksCount);
      landmarks.Clear();

      foreach (var nativeLandmark in source.AsReadOnlySpan())
      {
        landmarks.Add(new Landmark(nativeLandmark));
      }
      destination = new Landmarks(landmarks);
    }

    public void CloneTo(ref Landmarks destination)
    {
      if (landmarks == null)
      {
        destination = default;
        return;
      }

      var dstLandmarks = destination.landmarks ?? new List<Landmark>(landmarks.Count);
      dstLandmarks.Clear();
      dstLandmarks.AddRange(landmarks);

      destination = new Landmarks(dstLandmarks);
    }

    public override string ToString() => $"{{ \"landmarks\": {Util.Format(landmarks)} }}";
  }

  public readonly struct NormalizedLandmarks
  {
    public readonly List<NormalizedLandmark> landmarks;

    internal NormalizedLandmarks(List<NormalizedLandmark> landmarks)
    {
      this.landmarks = landmarks;
    }

    public static NormalizedLandmarks Alloc(int capacity) => new NormalizedLandmarks(new List<NormalizedLandmark>(capacity));

    public static NormalizedLandmarks CreateFrom(NormalizedLandmarkList proto)
    {
      var result = default(NormalizedLandmarks);
      Copy(proto, ref result);
      return result;
    }

    public static void Copy(NormalizedLandmarkList source, ref NormalizedLandmarks destination)
    {
      var landmarks = destination.landmarks ?? new List<NormalizedLandmark>(source.Landmark.Count);
      landmarks.Clear();
      for (var i = 0; i < source.Landmark.Count; i++)
      {
        landmarks.Add(NormalizedLandmark.CreateFrom(source.Landmark[i]));
      }

      destination = new NormalizedLandmarks(landmarks);
    }

    internal static void Copy(NativeNormalizedLandmarks source, ref NormalizedLandmarks destination)
    {
      var landmarks = destination.landmarks ?? new List<NormalizedLandmark>((int)source.landmarksCount);
      landmarks.Clear();

      foreach (var nativeLandmark in source.AsReadOnlySpan())
      {
        landmarks.Add(new NormalizedLandmark(nativeLandmark));
      }
      destination = new NormalizedLandmarks(landmarks);
    }

    public void CloneTo(ref NormalizedLandmarks destination)
    {
      if (landmarks == null)
      {
        destination = default;
        return;
      }

      var dstLandmarks = destination.landmarks ?? new List<NormalizedLandmark>(landmarks.Count);
      dstLandmarks.Clear();
      dstLandmarks.AddRange(landmarks);

      destination = new NormalizedLandmarks(dstLandmarks);
    }

    public override string ToString() => $"{{ \"landmarks\": {Util.Format(landmarks)} }}";
  }
}
