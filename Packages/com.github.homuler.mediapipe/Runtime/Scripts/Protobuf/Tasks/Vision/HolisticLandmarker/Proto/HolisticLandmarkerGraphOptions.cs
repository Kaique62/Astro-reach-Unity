// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: mediapipe/tasks/cc/vision/holistic_landmarker/proto/holistic_landmarker_graph_options.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Mediapipe.Tasks.Vision.HolisticLandmarker.Proto {

  /// <summary>Holder for reflection information generated from mediapipe/tasks/cc/vision/holistic_landmarker/proto/holistic_landmarker_graph_options.proto</summary>
  public static partial class HolisticLandmarkerGraphOptionsReflection {

    #region Descriptor
    /// <summary>File descriptor for mediapipe/tasks/cc/vision/holistic_landmarker/proto/holistic_landmarker_graph_options.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static HolisticLandmarkerGraphOptionsReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ClttZWRpYXBpcGUvdGFza3MvY2MvdmlzaW9uL2hvbGlzdGljX2xhbmRtYXJr",
            "ZXIvcHJvdG8vaG9saXN0aWNfbGFuZG1hcmtlcl9ncmFwaF9vcHRpb25zLnBy",
            "b3RvEjBtZWRpYXBpcGUudGFza3MudmlzaW9uLmhvbGlzdGljX2xhbmRtYXJr",
            "ZXIucHJvdG8aMG1lZGlhcGlwZS90YXNrcy9jYy9jb3JlL3Byb3RvL2Jhc2Vf",
            "b3B0aW9ucy5wcm90bxpPbWVkaWFwaXBlL3Rhc2tzL2NjL3Zpc2lvbi9mYWNl",
            "X2RldGVjdG9yL3Byb3RvL2ZhY2VfZGV0ZWN0b3JfZ3JhcGhfb3B0aW9ucy5w",
            "cm90bxpbbWVkaWFwaXBlL3Rhc2tzL2NjL3Zpc2lvbi9mYWNlX2xhbmRtYXJr",
            "ZXIvcHJvdG8vZmFjZV9sYW5kbWFya3NfZGV0ZWN0b3JfZ3JhcGhfb3B0aW9u",
            "cy5wcm90bxpbbWVkaWFwaXBlL3Rhc2tzL2NjL3Zpc2lvbi9oYW5kX2xhbmRt",
            "YXJrZXIvcHJvdG8vaGFuZF9sYW5kbWFya3NfZGV0ZWN0b3JfZ3JhcGhfb3B0",
            "aW9ucy5wcm90bxpXbWVkaWFwaXBlL3Rhc2tzL2NjL3Zpc2lvbi9oYW5kX2xh",
            "bmRtYXJrZXIvcHJvdG8vaGFuZF9yb2lfcmVmaW5lbWVudF9ncmFwaF9vcHRp",
            "b25zLnByb3RvGk9tZWRpYXBpcGUvdGFza3MvY2MvdmlzaW9uL3Bvc2VfZGV0",
            "ZWN0b3IvcHJvdG8vcG9zZV9kZXRlY3Rvcl9ncmFwaF9vcHRpb25zLnByb3Rv",
            "GlttZWRpYXBpcGUvdGFza3MvY2MvdmlzaW9uL3Bvc2VfbGFuZG1hcmtlci9w",
            "cm90by9wb3NlX2xhbmRtYXJrc19kZXRlY3Rvcl9ncmFwaF9vcHRpb25zLnBy",
            "b3RvIq0GCh5Ib2xpc3RpY0xhbmRtYXJrZXJHcmFwaE9wdGlvbnMSPQoMYmFz",
            "ZV9vcHRpb25zGAEgASgLMicubWVkaWFwaXBlLnRhc2tzLmNvcmUucHJvdG8u",
            "QmFzZU9wdGlvbnMSfgolaGFuZF9sYW5kbWFya3NfZGV0ZWN0b3JfZ3JhcGhf",
            "b3B0aW9ucxgCIAEoCzJPLm1lZGlhcGlwZS50YXNrcy52aXNpb24uaGFuZF9s",
            "YW5kbWFya2VyLnByb3RvLkhhbmRMYW5kbWFya3NEZXRlY3RvckdyYXBoT3B0",
            "aW9ucxJ2CiFoYW5kX3JvaV9yZWZpbmVtZW50X2dyYXBoX29wdGlvbnMYAyAB",
            "KAsySy5tZWRpYXBpcGUudGFza3MudmlzaW9uLmhhbmRfbGFuZG1hcmtlci5w",
            "cm90by5IYW5kUm9pUmVmaW5lbWVudEdyYXBoT3B0aW9ucxJpChtmYWNlX2Rl",
            "dGVjdG9yX2dyYXBoX29wdGlvbnMYBCABKAsyRC5tZWRpYXBpcGUudGFza3Mu",
            "dmlzaW9uLmZhY2VfZGV0ZWN0b3IucHJvdG8uRmFjZURldGVjdG9yR3JhcGhP",
            "cHRpb25zEn4KJWZhY2VfbGFuZG1hcmtzX2RldGVjdG9yX2dyYXBoX29wdGlv",
            "bnMYBSABKAsyTy5tZWRpYXBpcGUudGFza3MudmlzaW9uLmZhY2VfbGFuZG1h",
            "cmtlci5wcm90by5GYWNlTGFuZG1hcmtzRGV0ZWN0b3JHcmFwaE9wdGlvbnMS",
            "aQobcG9zZV9kZXRlY3Rvcl9ncmFwaF9vcHRpb25zGAYgASgLMkQubWVkaWFw",
            "aXBlLnRhc2tzLnZpc2lvbi5wb3NlX2RldGVjdG9yLnByb3RvLlBvc2VEZXRl",
            "Y3RvckdyYXBoT3B0aW9ucxJ+CiVwb3NlX2xhbmRtYXJrc19kZXRlY3Rvcl9n",
            "cmFwaF9vcHRpb25zGAcgASgLMk8ubWVkaWFwaXBlLnRhc2tzLnZpc2lvbi5w",
            "b3NlX2xhbmRtYXJrZXIucHJvdG8uUG9zZUxhbmRtYXJrc0RldGVjdG9yR3Jh",
            "cGhPcHRpb25zQmEKOmNvbS5nb29nbGUubWVkaWFwaXBlLnRhc2tzLnZpc2lv",
            "bi5ob2xpc3RpY2xhbmRtYXJrZXIucHJvdG9CI0hvbGlzdGljTGFuZG1hcmtl",
            "ckdyYXBoT3B0aW9uc1Byb3RvYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Mediapipe.Tasks.Core.Proto.BaseOptionsReflection.Descriptor, global::Mediapipe.Tasks.Vision.FaceDetector.Proto.FaceDetectorGraphOptionsReflection.Descriptor, global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceLandmarksDetectorGraphOptionsReflection.Descriptor, global::Mediapipe.Tasks.Vision.HandLandmarker.Proto.HandLandmarksDetectorGraphOptionsReflection.Descriptor, global::Mediapipe.Tasks.Vision.HandLandmarker.Proto.HandRoiRefinementGraphOptionsReflection.Descriptor, global::Mediapipe.Tasks.Vision.PoseDetector.Proto.PoseDetectorGraphOptionsReflection.Descriptor, global::Mediapipe.Tasks.Vision.PoseLandmarker.Proto.PoseLandmarksDetectorGraphOptionsReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Mediapipe.Tasks.Vision.HolisticLandmarker.Proto.HolisticLandmarkerGraphOptions), global::Mediapipe.Tasks.Vision.HolisticLandmarker.Proto.HolisticLandmarkerGraphOptions.Parser, new[]{ "BaseOptions", "HandLandmarksDetectorGraphOptions", "HandRoiRefinementGraphOptions", "FaceDetectorGraphOptions", "FaceLandmarksDetectorGraphOptions", "PoseDetectorGraphOptions", "PoseLandmarksDetectorGraphOptions" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class HolisticLandmarkerGraphOptions : pb::IMessage<HolisticLandmarkerGraphOptions>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<HolisticLandmarkerGraphOptions> _parser = new pb::MessageParser<HolisticLandmarkerGraphOptions>(() => new HolisticLandmarkerGraphOptions());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<HolisticLandmarkerGraphOptions> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Mediapipe.Tasks.Vision.HolisticLandmarker.Proto.HolisticLandmarkerGraphOptionsReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public HolisticLandmarkerGraphOptions() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public HolisticLandmarkerGraphOptions(HolisticLandmarkerGraphOptions other) : this() {
      baseOptions_ = other.baseOptions_ != null ? other.baseOptions_.Clone() : null;
      handLandmarksDetectorGraphOptions_ = other.handLandmarksDetectorGraphOptions_ != null ? other.handLandmarksDetectorGraphOptions_.Clone() : null;
      handRoiRefinementGraphOptions_ = other.handRoiRefinementGraphOptions_ != null ? other.handRoiRefinementGraphOptions_.Clone() : null;
      faceDetectorGraphOptions_ = other.faceDetectorGraphOptions_ != null ? other.faceDetectorGraphOptions_.Clone() : null;
      faceLandmarksDetectorGraphOptions_ = other.faceLandmarksDetectorGraphOptions_ != null ? other.faceLandmarksDetectorGraphOptions_.Clone() : null;
      poseDetectorGraphOptions_ = other.poseDetectorGraphOptions_ != null ? other.poseDetectorGraphOptions_.Clone() : null;
      poseLandmarksDetectorGraphOptions_ = other.poseLandmarksDetectorGraphOptions_ != null ? other.poseLandmarksDetectorGraphOptions_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public HolisticLandmarkerGraphOptions Clone() {
      return new HolisticLandmarkerGraphOptions(this);
    }

    /// <summary>Field number for the "base_options" field.</summary>
    public const int BaseOptionsFieldNumber = 1;
    private global::Mediapipe.Tasks.Core.Proto.BaseOptions baseOptions_;
    /// <summary>
    /// Base options for configuring MediaPipe Tasks, such as specifying the model
    /// asset bundle file with metadata, accelerator options, etc.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.Tasks.Core.Proto.BaseOptions BaseOptions {
      get { return baseOptions_; }
      set {
        baseOptions_ = value;
      }
    }

    /// <summary>Field number for the "hand_landmarks_detector_graph_options" field.</summary>
    public const int HandLandmarksDetectorGraphOptionsFieldNumber = 2;
    private global::Mediapipe.Tasks.Vision.HandLandmarker.Proto.HandLandmarksDetectorGraphOptions handLandmarksDetectorGraphOptions_;
    /// <summary>
    /// Options for hand landmarks graph.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.Tasks.Vision.HandLandmarker.Proto.HandLandmarksDetectorGraphOptions HandLandmarksDetectorGraphOptions {
      get { return handLandmarksDetectorGraphOptions_; }
      set {
        handLandmarksDetectorGraphOptions_ = value;
      }
    }

    /// <summary>Field number for the "hand_roi_refinement_graph_options" field.</summary>
    public const int HandRoiRefinementGraphOptionsFieldNumber = 3;
    private global::Mediapipe.Tasks.Vision.HandLandmarker.Proto.HandRoiRefinementGraphOptions handRoiRefinementGraphOptions_;
    /// <summary>
    /// Options for hand roi refinement graph.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.Tasks.Vision.HandLandmarker.Proto.HandRoiRefinementGraphOptions HandRoiRefinementGraphOptions {
      get { return handRoiRefinementGraphOptions_; }
      set {
        handRoiRefinementGraphOptions_ = value;
      }
    }

    /// <summary>Field number for the "face_detector_graph_options" field.</summary>
    public const int FaceDetectorGraphOptionsFieldNumber = 4;
    private global::Mediapipe.Tasks.Vision.FaceDetector.Proto.FaceDetectorGraphOptions faceDetectorGraphOptions_;
    /// <summary>
    /// Options for face detector graph.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.Tasks.Vision.FaceDetector.Proto.FaceDetectorGraphOptions FaceDetectorGraphOptions {
      get { return faceDetectorGraphOptions_; }
      set {
        faceDetectorGraphOptions_ = value;
      }
    }

    /// <summary>Field number for the "face_landmarks_detector_graph_options" field.</summary>
    public const int FaceLandmarksDetectorGraphOptionsFieldNumber = 5;
    private global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceLandmarksDetectorGraphOptions faceLandmarksDetectorGraphOptions_;
    /// <summary>
    /// Options for face landmarks detector graph.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceLandmarksDetectorGraphOptions FaceLandmarksDetectorGraphOptions {
      get { return faceLandmarksDetectorGraphOptions_; }
      set {
        faceLandmarksDetectorGraphOptions_ = value;
      }
    }

    /// <summary>Field number for the "pose_detector_graph_options" field.</summary>
    public const int PoseDetectorGraphOptionsFieldNumber = 6;
    private global::Mediapipe.Tasks.Vision.PoseDetector.Proto.PoseDetectorGraphOptions poseDetectorGraphOptions_;
    /// <summary>
    /// Options for pose detector graph.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.Tasks.Vision.PoseDetector.Proto.PoseDetectorGraphOptions PoseDetectorGraphOptions {
      get { return poseDetectorGraphOptions_; }
      set {
        poseDetectorGraphOptions_ = value;
      }
    }

    /// <summary>Field number for the "pose_landmarks_detector_graph_options" field.</summary>
    public const int PoseLandmarksDetectorGraphOptionsFieldNumber = 7;
    private global::Mediapipe.Tasks.Vision.PoseLandmarker.Proto.PoseLandmarksDetectorGraphOptions poseLandmarksDetectorGraphOptions_;
    /// <summary>
    /// Options for pose landmarks detector graph.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.Tasks.Vision.PoseLandmarker.Proto.PoseLandmarksDetectorGraphOptions PoseLandmarksDetectorGraphOptions {
      get { return poseLandmarksDetectorGraphOptions_; }
      set {
        poseLandmarksDetectorGraphOptions_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as HolisticLandmarkerGraphOptions);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(HolisticLandmarkerGraphOptions other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(BaseOptions, other.BaseOptions)) return false;
      if (!object.Equals(HandLandmarksDetectorGraphOptions, other.HandLandmarksDetectorGraphOptions)) return false;
      if (!object.Equals(HandRoiRefinementGraphOptions, other.HandRoiRefinementGraphOptions)) return false;
      if (!object.Equals(FaceDetectorGraphOptions, other.FaceDetectorGraphOptions)) return false;
      if (!object.Equals(FaceLandmarksDetectorGraphOptions, other.FaceLandmarksDetectorGraphOptions)) return false;
      if (!object.Equals(PoseDetectorGraphOptions, other.PoseDetectorGraphOptions)) return false;
      if (!object.Equals(PoseLandmarksDetectorGraphOptions, other.PoseLandmarksDetectorGraphOptions)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (baseOptions_ != null) hash ^= BaseOptions.GetHashCode();
      if (handLandmarksDetectorGraphOptions_ != null) hash ^= HandLandmarksDetectorGraphOptions.GetHashCode();
      if (handRoiRefinementGraphOptions_ != null) hash ^= HandRoiRefinementGraphOptions.GetHashCode();
      if (faceDetectorGraphOptions_ != null) hash ^= FaceDetectorGraphOptions.GetHashCode();
      if (faceLandmarksDetectorGraphOptions_ != null) hash ^= FaceLandmarksDetectorGraphOptions.GetHashCode();
      if (poseDetectorGraphOptions_ != null) hash ^= PoseDetectorGraphOptions.GetHashCode();
      if (poseLandmarksDetectorGraphOptions_ != null) hash ^= PoseLandmarksDetectorGraphOptions.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (baseOptions_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(BaseOptions);
      }
      if (handLandmarksDetectorGraphOptions_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(HandLandmarksDetectorGraphOptions);
      }
      if (handRoiRefinementGraphOptions_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(HandRoiRefinementGraphOptions);
      }
      if (faceDetectorGraphOptions_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(FaceDetectorGraphOptions);
      }
      if (faceLandmarksDetectorGraphOptions_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(FaceLandmarksDetectorGraphOptions);
      }
      if (poseDetectorGraphOptions_ != null) {
        output.WriteRawTag(50);
        output.WriteMessage(PoseDetectorGraphOptions);
      }
      if (poseLandmarksDetectorGraphOptions_ != null) {
        output.WriteRawTag(58);
        output.WriteMessage(PoseLandmarksDetectorGraphOptions);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (baseOptions_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(BaseOptions);
      }
      if (handLandmarksDetectorGraphOptions_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(HandLandmarksDetectorGraphOptions);
      }
      if (handRoiRefinementGraphOptions_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(HandRoiRefinementGraphOptions);
      }
      if (faceDetectorGraphOptions_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(FaceDetectorGraphOptions);
      }
      if (faceLandmarksDetectorGraphOptions_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(FaceLandmarksDetectorGraphOptions);
      }
      if (poseDetectorGraphOptions_ != null) {
        output.WriteRawTag(50);
        output.WriteMessage(PoseDetectorGraphOptions);
      }
      if (poseLandmarksDetectorGraphOptions_ != null) {
        output.WriteRawTag(58);
        output.WriteMessage(PoseLandmarksDetectorGraphOptions);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (baseOptions_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(BaseOptions);
      }
      if (handLandmarksDetectorGraphOptions_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(HandLandmarksDetectorGraphOptions);
      }
      if (handRoiRefinementGraphOptions_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(HandRoiRefinementGraphOptions);
      }
      if (faceDetectorGraphOptions_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(FaceDetectorGraphOptions);
      }
      if (faceLandmarksDetectorGraphOptions_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(FaceLandmarksDetectorGraphOptions);
      }
      if (poseDetectorGraphOptions_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(PoseDetectorGraphOptions);
      }
      if (poseLandmarksDetectorGraphOptions_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(PoseLandmarksDetectorGraphOptions);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(HolisticLandmarkerGraphOptions other) {
      if (other == null) {
        return;
      }
      if (other.baseOptions_ != null) {
        if (baseOptions_ == null) {
          BaseOptions = new global::Mediapipe.Tasks.Core.Proto.BaseOptions();
        }
        BaseOptions.MergeFrom(other.BaseOptions);
      }
      if (other.handLandmarksDetectorGraphOptions_ != null) {
        if (handLandmarksDetectorGraphOptions_ == null) {
          HandLandmarksDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.HandLandmarker.Proto.HandLandmarksDetectorGraphOptions();
        }
        HandLandmarksDetectorGraphOptions.MergeFrom(other.HandLandmarksDetectorGraphOptions);
      }
      if (other.handRoiRefinementGraphOptions_ != null) {
        if (handRoiRefinementGraphOptions_ == null) {
          HandRoiRefinementGraphOptions = new global::Mediapipe.Tasks.Vision.HandLandmarker.Proto.HandRoiRefinementGraphOptions();
        }
        HandRoiRefinementGraphOptions.MergeFrom(other.HandRoiRefinementGraphOptions);
      }
      if (other.faceDetectorGraphOptions_ != null) {
        if (faceDetectorGraphOptions_ == null) {
          FaceDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.FaceDetector.Proto.FaceDetectorGraphOptions();
        }
        FaceDetectorGraphOptions.MergeFrom(other.FaceDetectorGraphOptions);
      }
      if (other.faceLandmarksDetectorGraphOptions_ != null) {
        if (faceLandmarksDetectorGraphOptions_ == null) {
          FaceLandmarksDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceLandmarksDetectorGraphOptions();
        }
        FaceLandmarksDetectorGraphOptions.MergeFrom(other.FaceLandmarksDetectorGraphOptions);
      }
      if (other.poseDetectorGraphOptions_ != null) {
        if (poseDetectorGraphOptions_ == null) {
          PoseDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.PoseDetector.Proto.PoseDetectorGraphOptions();
        }
        PoseDetectorGraphOptions.MergeFrom(other.PoseDetectorGraphOptions);
      }
      if (other.poseLandmarksDetectorGraphOptions_ != null) {
        if (poseLandmarksDetectorGraphOptions_ == null) {
          PoseLandmarksDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.PoseLandmarker.Proto.PoseLandmarksDetectorGraphOptions();
        }
        PoseLandmarksDetectorGraphOptions.MergeFrom(other.PoseLandmarksDetectorGraphOptions);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (baseOptions_ == null) {
              BaseOptions = new global::Mediapipe.Tasks.Core.Proto.BaseOptions();
            }
            input.ReadMessage(BaseOptions);
            break;
          }
          case 18: {
            if (handLandmarksDetectorGraphOptions_ == null) {
              HandLandmarksDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.HandLandmarker.Proto.HandLandmarksDetectorGraphOptions();
            }
            input.ReadMessage(HandLandmarksDetectorGraphOptions);
            break;
          }
          case 26: {
            if (handRoiRefinementGraphOptions_ == null) {
              HandRoiRefinementGraphOptions = new global::Mediapipe.Tasks.Vision.HandLandmarker.Proto.HandRoiRefinementGraphOptions();
            }
            input.ReadMessage(HandRoiRefinementGraphOptions);
            break;
          }
          case 34: {
            if (faceDetectorGraphOptions_ == null) {
              FaceDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.FaceDetector.Proto.FaceDetectorGraphOptions();
            }
            input.ReadMessage(FaceDetectorGraphOptions);
            break;
          }
          case 42: {
            if (faceLandmarksDetectorGraphOptions_ == null) {
              FaceLandmarksDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceLandmarksDetectorGraphOptions();
            }
            input.ReadMessage(FaceLandmarksDetectorGraphOptions);
            break;
          }
          case 50: {
            if (poseDetectorGraphOptions_ == null) {
              PoseDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.PoseDetector.Proto.PoseDetectorGraphOptions();
            }
            input.ReadMessage(PoseDetectorGraphOptions);
            break;
          }
          case 58: {
            if (poseLandmarksDetectorGraphOptions_ == null) {
              PoseLandmarksDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.PoseLandmarker.Proto.PoseLandmarksDetectorGraphOptions();
            }
            input.ReadMessage(PoseLandmarksDetectorGraphOptions);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            if (baseOptions_ == null) {
              BaseOptions = new global::Mediapipe.Tasks.Core.Proto.BaseOptions();
            }
            input.ReadMessage(BaseOptions);
            break;
          }
          case 18: {
            if (handLandmarksDetectorGraphOptions_ == null) {
              HandLandmarksDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.HandLandmarker.Proto.HandLandmarksDetectorGraphOptions();
            }
            input.ReadMessage(HandLandmarksDetectorGraphOptions);
            break;
          }
          case 26: {
            if (handRoiRefinementGraphOptions_ == null) {
              HandRoiRefinementGraphOptions = new global::Mediapipe.Tasks.Vision.HandLandmarker.Proto.HandRoiRefinementGraphOptions();
            }
            input.ReadMessage(HandRoiRefinementGraphOptions);
            break;
          }
          case 34: {
            if (faceDetectorGraphOptions_ == null) {
              FaceDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.FaceDetector.Proto.FaceDetectorGraphOptions();
            }
            input.ReadMessage(FaceDetectorGraphOptions);
            break;
          }
          case 42: {
            if (faceLandmarksDetectorGraphOptions_ == null) {
              FaceLandmarksDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.FaceLandmarker.Proto.FaceLandmarksDetectorGraphOptions();
            }
            input.ReadMessage(FaceLandmarksDetectorGraphOptions);
            break;
          }
          case 50: {
            if (poseDetectorGraphOptions_ == null) {
              PoseDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.PoseDetector.Proto.PoseDetectorGraphOptions();
            }
            input.ReadMessage(PoseDetectorGraphOptions);
            break;
          }
          case 58: {
            if (poseLandmarksDetectorGraphOptions_ == null) {
              PoseLandmarksDetectorGraphOptions = new global::Mediapipe.Tasks.Vision.PoseLandmarker.Proto.PoseLandmarksDetectorGraphOptions();
            }
            input.ReadMessage(PoseLandmarksDetectorGraphOptions);
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
