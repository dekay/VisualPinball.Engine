﻿// Visual Pinball Engine
// Copyright (C) 2021 freezy and VPE Team
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.

using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace VisualPinball.Unity
{
	[BurstCompile]
	internal struct ColliderAllocationJob : IJob
	{
		[ReadOnly] public NativeList<CircleCollider> CircleColliders;
		[ReadOnly] public NativeList<FlipperCollider> FlipperColliders;
		[ReadOnly] public NativeList<GateCollider> GateColliders;
		[ReadOnly] public NativeList<Line3DCollider> Line3DColliders;
		[ReadOnly] public NativeList<LineSlingshotCollider> LineSlingshotColliders;
		[ReadOnly] public NativeList<LineCollider> LineColliders;
		[ReadOnly] public NativeList<LineZCollider> LineZColliders;
		[ReadOnly] public NativeList<PlungerCollider> PlungerColliders;
		[ReadOnly] public NativeList<PointCollider> PointColliders;
		[ReadOnly] public NativeList<SpinnerCollider> SpinnerColliders;
		[ReadOnly] public NativeList<TriangleCollider> TriangleColliders;
		[ReadOnly] public NativeArray<PlaneCollider> PlaneColliders;

		public NativeArray<BlobAssetReference<ColliderBlob>> BlobAsset;

		public ColliderAllocationJob(NativeList<CircleCollider> circleColliders, NativeList<FlipperCollider> flipperColliders,
			NativeList<GateCollider> gateColliders, NativeList<Line3DCollider> line3DColliders,
			NativeList<LineSlingshotCollider> lineSlingshotColliders, NativeList<LineCollider> lineColliders,
			NativeList<LineZCollider> lineZColliders, NativeList<PlungerCollider> plungerColliders,
			NativeList<PointCollider> pointColliders, NativeList<SpinnerCollider> spinnerColliders,
			NativeList<TriangleCollider> triangleColliders, NativeArray<PlaneCollider> planeColliders) : this()
		{
			CircleColliders = circleColliders;
			FlipperColliders = flipperColliders;
			GateColliders = gateColliders;
			Line3DColliders = line3DColliders;
			LineSlingshotColliders = lineSlingshotColliders;
			LineColliders = lineColliders;
			LineZColliders = lineZColliders;
			PlungerColliders = plungerColliders;
			PointColliders = pointColliders;
			SpinnerColliders = spinnerColliders;
			TriangleColliders = triangleColliders;
			PlaneColliders = planeColliders;

			BlobAsset = new NativeArray<BlobAssetReference<ColliderBlob>>(1, Allocator.TempJob);
		}
		public void Execute()
		{
			var builder = new BlobBuilder(Allocator.Temp);
			var colliderId = 0;
			ref var root = ref builder.ConstructRoot<ColliderBlob>();
			var count = CircleColliders.Length + FlipperColliders.Length + GateColliders.Length + Line3DColliders.Length
			            + LineSlingshotColliders.Length + LineColliders.Length + LineZColliders.Length + PlungerColliders.Length
			            + PointColliders.Length + SpinnerColliders.Length + TriangleColliders.Length + PlaneColliders.Length;

			var colliders = builder.Allocate(ref root.Colliders, count);

			PlaneColliders[0].Allocate(builder, ref colliders, colliderId++);
			PlaneColliders[1].Allocate(builder, ref colliders, colliderId++);

			root.PlayfieldColliderId = PlaneColliders[0].Id;
			root.GlassColliderId = PlaneColliders[1].Id;

			// copy generated colliders into blob array
			for (var i = 0; i < CircleColliders.Length; i++) {
				CircleColliders[i].Allocate(builder, ref colliders, colliderId++);
			}
			for (var i = 0; i < FlipperColliders.Length; i++) {
				FlipperColliders[i].Allocate(builder, ref colliders, colliderId++);
			}
			for (var i = 0; i < GateColliders.Length; i++) {
				GateColliders[i].Allocate(builder, ref colliders, colliderId++);
			}
			for (var i = 0; i < Line3DColliders.Length; i++) {
				Line3DColliders[i].Allocate(builder, ref colliders, colliderId++);
			}
			for (var i = 0; i < LineSlingshotColliders.Length; i++) {
				LineSlingshotColliders[i].Allocate(builder, ref colliders, colliderId++);
			}
			for (var i = 0; i < LineColliders.Length; i++) {
				LineColliders[i].Allocate(builder, ref colliders, colliderId++);
			}
			for (var i = 0; i < LineZColliders.Length; i++) {
				LineZColliders[i].Allocate(builder, ref colliders, colliderId++);
			}
			for (var i = 0; i < PlungerColliders.Length; i++) {
				PlungerColliders[i].Allocate(builder, ref colliders, colliderId++);
			}
			for (var i = 0; i < PointColliders.Length; i++) {
				PointColliders[i].Allocate(builder, ref colliders, colliderId++);
			}
			for (var i = 0; i < SpinnerColliders.Length; i++) {
				SpinnerColliders[i].Allocate(builder, ref colliders, colliderId++);
			}
			for (var i = 0; i < TriangleColliders.Length; i++) {
				TriangleColliders[i].Allocate(builder, ref colliders, colliderId++);
			}

			BlobAsset[0] = builder.CreateBlobAssetReference<ColliderBlob>(Allocator.Persistent);
			builder.Dispose();
		}
	}
}
