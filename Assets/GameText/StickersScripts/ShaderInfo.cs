using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShaderInfo_Namespace
{

	public struct ShaderInfo
	{
		public float StickerType;
		public float MotionState;

		public Color BorderColor;
		public float BorderSizeOne;
		public float BorderSizeTwo;
		public float BorderBlurriness;
	
		public float RangeSTen_Ten0;
		public float RangeSTen_Ten1;
		public float RangeSTen_Ten2;
		public float RangeSTen_Ten3;
	
		public float RangeSOne_One0;
		public float RangeSOne_One1;
		public float RangeSOne_One2;
		public float RangeSOne_One3;
	}

	public struct ShaderInfoSprite
	{
    	public float InVariableTick;
    	public float InVariableRatioX;
    	public float InVariableRatioY;
    	public float OutlineSprite;
		public Color OutlineColor;
	}

}




namespace StickerName_Namespace
{
	public static class StickerNameClass
	{

		public static string[] StickerNameStringArray;
		public static string[] ShaderPathNameStringArrayOne;
		public static string[] ShaderPathNameStringArrayTwo;

		


		public static void SetStickerNameStringArray()
		{
			StickerNameStringArray = new string[]
			{
				"Arc",
				"Arrow", 
				"BlobbyCross",
				"BoxRounded",
				"CapsuleUneven",
				"CircleCross",
				"Cross",
				"CutDisk",
				"DollarSign",
				"Egg",
				"EllipseHorizontal",
				"Gradient2D",
				"Heart",
				"Hexagon",
				"Horseshoe",
				"Hyperbola",
				"Joint2DSphere",
				"Joint2DFlat",
				"MinusShape",
				"Moon",
				"OrientedBox",
				"Parabola",
				"Parallelogram",
				"ParallelogramRounded",
				"Pie",
				"QuadParameter",
				"QuadraticCircle",
				"Rhombus",
				"Ring",
				"RoundedBox",
				"RoundedX",
				"Segment",
				"Squircle",
				"Stairs",
				"StarN",
				"Star",
				"Triangle2D",
				"TriangleForm",
				"TriangleIsosceles",
				"TriangleRounded",
				"Trapezoid",
				"Tunnel",
				"Vesica",
				"VessicaSegment",

			};

		}


		public static string[] GetStickerNameStringArray()
		{
			return StickerNameStringArray;
		}


		public static void SetShaderPathNameStringArray()
		{
			
			ShaderPathNameStringArrayOne = new string[]
			{
				"ShaderBloom/ContainerShader-BallOfFire",
				"ShaderBloom/ContainerShader-BookShelf",
				"ShaderBloom/ContainerShader-BubblingPuls",
				"ShaderBloom/ContainerShader-Waves",
				"ShaderBloom/ContainerShader-PlasmaFlower",
				"ShaderBloom/ContainerShader-MandelFire",
				"ShaderBloom/ContainerShader-FireAndWater",
				"ShaderBloom/ContainerShader-Noise2D",
				"ShaderBloom/ContainerShader-PlanetSpace",
				"ShaderBloom/ContainerShader-Star",
				"ShaderBloom/ContainerShader-CirclesDisco",
				"ShaderBloom/ContainerShader-PaintTexture",
				"ShaderBloom/ContainerShader-FractalPyramid",
				"ShaderBloom/ContainerShader-Bubble",
				"ShaderBloom/ContainerShader-StarFractal",
				"ShaderBloom/ContainerShader-WetNeural",
				"ShaderBloom/ContainerShader-PulsatingPink",
				"ShaderBloom/ContainerShader-LaserBeam",
				"ShaderBloom/ContainerShader-Clouds",
				"ShaderBloom/ContainerShader-GlowingMarbling",
				"ShaderBloom/ContainerShader-PortalGreen",
				"ShaderBloom/ContainerShader-SimplicityGalaxy",
				"ShaderBloom/ContainerShader-DigitalBrain",
				"ShaderBloom/ContainerShader-SpiralRainbow",
				"ShaderBloom/ContainerShader-FurSphere",
				"ShaderBloom/ContainerShader-GlowingBlobs",
				"ShaderBloom/ContainerShader-XBall",
				"ShaderBloom/ContainerShader-WarpSpeed",
				"ShaderBloom/ContainerShader-70SPaint",
				"ShaderBloom/ContainerShader-BlueCell",
				"ShaderBloom/ContainerShader-Lightning",
				"ShaderBloom/ContainerShader-Chlast",
				"ShaderBloom/ContainerShader-Interstellar",
				"ShaderBloom/ContainerShader-Techtacles",
				"ShaderBloom/ContainerShader-TwoPlanes",
				"ShaderBloom/ContainerShader-RainbowSpiral",
				"ShaderBloom/ContainerShader-DarkSpiral",
				"ShaderBloom/ContainerShader-SimplePlasma",
				"ShaderBloom/ContainerShader-Thunders",
				"ShaderBloom/ContainerShader-SpectrumBeam",
				"ShaderBloom/ContainerShader-PadEmulator",
				"ShaderBloom/ContainerShader-Dodecahedron",
				"ShaderBloom/ContainerShader-FractalLight",
				"ShaderBloom/ContainerShader-GalacticSpiral",
				"ShaderBloom/ContainerShader-EyeSauron",
				"ShaderBloom/ContainerShader-Moire",
				"ShaderBloom/ContainerShader-Flaring",
				"ShaderBloom/ContainerShader-SpinnerGeometry",
				"ShaderBloom/ContainerShader-CasinoStyle",
				"ShaderBloom/ContainerShader-PulsingMaldelbox"

			};

		}

		public static string[] GetShaderPathNameStringArray()
		{
			return ShaderPathNameStringArrayOne;
		}





		public static void SetShaderPathNameStringArrayTwo()
		{
			
			ShaderPathNameStringArrayTwo = new string[]
			{

				"ShaderBloom/ContainerShader-Gool",
				"ShaderBloom/ContainerShader-SpheresReflect",
				"ShaderBloom/ContainerShader-Blocks",
				"ShaderBloom/ContainerShader-FractalSymmetry",
				"ShaderBloom/ContainerShader-RainbowSpaguetti",
				"ShaderBloom/ContainerShader-Satori",
				"ShaderBloom/ContainerShader-Truchet",
				"ShaderBloom/ContainerShader-HSL_COLOR",
				"ShaderBloom/ContainerShader-GoldDragon",
				"ShaderBloom/ContainerShader-ShaderforthLace",
				"ShaderBloom/ContainerShader-LogSpiral",
				"ShaderBloom/ContainerShader-ChessHipno",
				"ShaderBloom/ContainerShader-MusicBox",
				"ShaderBloom/ContainerShader-PersianCarpet",
				"ShaderBloom/ContainerShader-PulseGeometry",
				"ShaderBloom/ContainerShader-Shiny",
				"ShaderBloom/ContainerShader-Metaballs",
				"ShaderBloom/ContainerShader-2DVoxel",
				"ShaderBloom/ContainerShader-StarSwirl",
				"ShaderBloom/ContainerShader-SpectrumRing",
				"ShaderBloom/ContainerShader-RaveSpiral",
				"ShaderBloom/ContainerShader-GlassPolyhedron",
				"ShaderBloom/ContainerShader-NoiseAnimation",
				"ShaderBloom/ContainerShader-CellShading",
				"ShaderBloom/ContainerShader-Topologica",
				"ShaderBloom/ContainerShader-Watery",
				"ShaderBloom/ContainerShader-Squiggles",
				"ShaderBloom/ContainerShader-DaRasterizer",
				"ShaderBloom/ContainerShader-SphereMeta",
				"ShaderBloom/ContainerShader-FMS_Cat",
				"ShaderBloom/ContainerShader-SDFCUBE",
				"ShaderBloom/ContainerShader-SquareStyle",
				"ShaderBloom/ContainerShader-ColorWheel",
				"ShaderBloom/ContainerShader-Hexagons",
				"ShaderBloom/ContainerShader-InfiniteSpheres",
				"ShaderBloom/ContainerShader-Toto",
				"ShaderBloom/ContainerShader-TechnoStyle",
				"ShaderBloom/ContainerShader-Parametrics",
				"ShaderBloom/ContainerShader-StarShiny",
				"ShaderBloom/ContainerShader-PlasmaGlobe",
				"ShaderBloom/ContainerShader-ReproductionIII",
				"ShaderBloom/ContainerShader-StarDust",
				"ShaderBloom/ContainerShader-PolygonsPatterns",
				"ShaderBloom/ContainerShader-ToonSmile",
				"ShaderBloom/ContainerShader-WorleyNoise",
				"ShaderBloom/ContainerShader-EnergyLife",
				"ShaderBloom/ContainerShader-WorleyNoiseType",
				"ShaderBloom/ContainerShader-ColoredBars",
				"ShaderBloom/ContainerShader-FractalStyle",
				"ShaderBloom/ContainerShader-Door",
				"ShaderBloom/ContainerShader-GridDash",






			};

		}

		public static string[] GetShaderPathNameStringArrayTwo()
		{
			return ShaderPathNameStringArrayTwo;
		}




	}

}
namespace ShaderName_Enum_Namespace
{
	public enum ShaderName_Enum 
	{
		BallOfFire,
		BookShelf,
		BubblingPuls,
		Waves,
		PlasmaFlower,
		MandelFire,
		FireAndWater,
		Noise2D,
		PlanetSpace,
		Star,
		CirclesDisco,
		PaintTexture,
		FractalPyramid,
		Bubble,
		StarFractal,
		WetNeural,
		PulsatingPink,
		LaserBeam,
		Clouds,
		GlowingMarbling,
		PortalGreen,
		SimplicityGalaxy,
		DigitalBrain,
		SpiralRainbow,
		FurSphere,
		GlowingBlobs,
		XBall,
		WarpSpeed,
		_70SPaint,
		BlueCell,
		Lightning,
		Chlast,
		Interstellar,
		Techtacles,
		TwoPlanes,
		RainbowSpiral,
		DarkSpiral,
		SimplePlasma,
		Thunders,
		SpectrumBeam,
		PadEmulator,
		Dodecahedron,
		FractalLight,
		GalacticSpiral,
		EyeSauron,
		Moire,
		Flaring,
		SpinnerGeometry,
		CasinoStyle,
		PulsingMaldelbo,
		Gool,
		SpheresReflect,
		Blocks,
		FractalSymmetry,
		RainbowSpaguetti,
		Satori,
		Truchet,
		HSL_COLOR,
		GoldDragon,
		ShaderforthLace,
		LogSpiral,
		ChessHipno,
		MusicBox,
		PersianCarpet,
		PulseGeometry,
		Shiny,
		Metaballs,
		_2DVoxel,
		StarSwirl,
		SpectrumRing,
		RaveSpiral,
		GlassPolyhedron,
		NoiseAnimation,
		CellShading,
		Topologica,
		Watery,
		Squiggles,
		DaRasterizer,
		SphereMeta,
		FMS_Cat,
		SDFCUBE,
		SquareStyle,
		ColorWheel,
		Hexagons,
		InfiniteSpheres,
		Toto,
		TechnoStyle,
		Parametrics,
		StarShiny,
		PlasmaGlobe,
		ReproductionIII,
		StarDust,
		PolygonsPatterns,
		ToonSmile,
		WorleyNoise,
		EnergyLife,
		WorleyNoiseType,
		ColoredBars,
		FractalStyle,
		Door,
		GridDash

	};
}
