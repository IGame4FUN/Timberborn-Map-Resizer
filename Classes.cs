using System.Collections.Generic;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public class Size {
    [JsonPropertyName("X")]
    public int X { get; set; }

    [JsonPropertyName("Y")]
    public int Y { get; set; }
}

public class MapSize {
    [JsonPropertyName("Size")]
    public Size Size { get; set; }
}

public class Heights {
    [JsonPropertyName("Array")]
    public string Array { get; set; }
}

public class TerrainMap {
    [JsonPropertyName("Heights")]
    public Heights Heights { get; set; }
}

public class Target {
    [JsonPropertyName("X")]
    public double X { get; set; }

    [JsonPropertyName("Y")]
    public double Y { get; set; }

    [JsonPropertyName("Z")]
    public double Z { get; set; }
}

public class SavedCameraState {
    [JsonPropertyName("Target")]
    public Target Target { get; set; }

    [JsonPropertyName("ZoomLevel")]
    public double ZoomLevel { get; set; }

    [JsonPropertyName("HorizontalAngle")]
    public double HorizontalAngle { get; set; }

    [JsonPropertyName("VerticalAngle")]
    public double VerticalAngle { get; set; }
}

public class CameraStateRestorer {
    [JsonPropertyName("SavedCameraState")]
    public SavedCameraState SavedCameraState { get; set; }
}

public class WaterDepths {
    [JsonPropertyName("Array")]
    public string Array { get; set; }
}

public class Outflows {
    [JsonPropertyName("Array")]
    public string Array { get; set; }
}

public class WaterMap {
    [JsonPropertyName("WaterDepths")]
    public WaterDepths WaterDepths { get; set; }

    [JsonPropertyName("Outflows")]
    public Outflows Outflows { get; set; }
}

public class MoistureLevels {
    [JsonPropertyName("Array")]
    public string Array { get; set; }
}

public class SoilMoistureSimulator {
    [JsonPropertyName("MoistureLevels")]
    public MoistureLevels MoistureLevels { get; set; }
}

public class Singletons {
    [JsonPropertyName("MapSize")]
    public MapSize MapSize { get; set; }

    [JsonPropertyName("TerrainMap")]
    public TerrainMap TerrainMap { get; set; }

    [JsonPropertyName("CameraStateRestorer")]
    public CameraStateRestorer CameraStateRestorer { get; set; }

    [JsonPropertyName("WaterMap")]
    public WaterMap WaterMap { get; set; }

    [JsonPropertyName("SoilMoistureSimulator")]
    public SoilMoistureSimulator SoilMoistureSimulator { get; set; }
}

public class Coordinates {
    [JsonPropertyName("X")]
    public int X { get; set; }

    [JsonPropertyName("Y")]
    public int Y { get; set; }

    [JsonPropertyName("Z")]
    public int Z { get; set; }
}

public class Orientation {
    [JsonPropertyName("Value")]
    public string Value { get; set; }
}

public class BlockObject {
    [JsonPropertyName("Coordinates")]
    public Coordinates Coordinates { get; set; }

    [JsonPropertyName("Orientation")]
    public Orientation Orientation { get; set; }
}

public class ConstructionSite {
    [JsonPropertyName("BuildTimeProgressInHoursKey")]
    public double BuildTimeProgressInHoursKey { get; set; }
}

public class Constructible {
    [JsonPropertyName("Finished")]
    public bool Finished { get; set; }
}

public class WaterSource {
    [JsonPropertyName("SpecifiedStrength")]
    public double SpecifiedStrength { get; set; }

    [JsonPropertyName("CurrentStrength")]
    public double CurrentStrength { get; set; }
}

public class Growable {
    [JsonPropertyName("GrowthProgress")]
    public double GrowthProgress { get; set; }
}

public class CoordinatesOffset {
    [JsonPropertyName("X")]
    public double X { get; set; }

    [JsonPropertyName("Y")]
    public double Y { get; set; }
}

public class CoordinatesOffseter {
    [JsonPropertyName("CoordinatesOffset")]
    public CoordinatesOffset CoordinatesOffset { get; set; }
}

public class NaturalResourceModelRandomizer {
    [JsonPropertyName("Rotation")]
    public double Rotation { get; set; }

    [JsonPropertyName("DiameterScale")]
    public double DiameterScale { get; set; }

    [JsonPropertyName("HeightScale")]
    public double HeightScale { get; set; }
}

public class Good {
    [JsonPropertyName("Id")]
    public string Id { get; set; }
}

public class Yield {
    [JsonPropertyName("Good")]
    public Good Good { get; set; }

    [JsonPropertyName("Amount")]
    public int Amount { get; set; }
}

public class YielderGatherable {
    [JsonPropertyName("Yield")]
    public Yield Yield { get; set; }
}

public class GatherableYieldGrower {
    [JsonPropertyName("GrowthProgress")]
    public double GrowthProgress { get; set; }
}

public class DryObject {
    [JsonPropertyName("IsDry")]
    public bool IsDry { get; set; }
}

public class LivingNaturalResource {
    [JsonPropertyName("IsDead")]
    public bool IsDead { get; set; }
}

public class YielderCuttable {
    [JsonPropertyName("Yield")]
    public Yield Yield { get; set; }
}

public class YielderRuin {
    [JsonPropertyName("Yield")]
    public Yield Yield { get; set; }
}

public class RuinModels {
    [JsonPropertyName("VariantId")]
    public string VariantId { get; set; }
}

public class Components {
    [JsonPropertyName("BlockObject")]
    public BlockObject BlockObject { get; set; }

    [JsonPropertyName("ConstructionSite")]
    public ConstructionSite ConstructionSite { get; set; }

    [JsonPropertyName("Constructible")]
    public Constructible Constructible { get; set; }

    [JsonPropertyName("WaterSource")]
    public WaterSource WaterSource { get; set; }

    [JsonPropertyName("Growable")]
    public Growable Growable { get; set; }

    [JsonPropertyName("CoordinatesOffseter")]
    public CoordinatesOffseter CoordinatesOffseter { get; set; }

    [JsonPropertyName("NaturalResourceModelRandomizer")]
    public NaturalResourceModelRandomizer NaturalResourceModelRandomizer { get; set; }

    [JsonPropertyName("Yielder:Gatherable")]
    public YielderGatherable YielderGatherable { get; set; }

    [JsonPropertyName("GatherableYieldGrower")]
    public GatherableYieldGrower GatherableYieldGrower { get; set; }

    [JsonPropertyName("DryObject")]
    public DryObject DryObject { get; set; }

    [JsonPropertyName("LivingNaturalResource")]
    public LivingNaturalResource LivingNaturalResource { get; set; }

    [JsonPropertyName("Yielder:Cuttable")]
    public YielderCuttable YielderCuttable { get; set; }

    [JsonPropertyName("Yielder:Ruin")]
    public YielderRuin YielderRuin { get; set; }

    [JsonPropertyName("RuinModels")]
    public RuinModels RuinModels { get; set; }
}

public class Entity {
    //[JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonPropertyName("Id")]
    public string Id { get; set; }

    [JsonPropertyName("Template")]
    public string Template { get; set; }

    [JsonPropertyName("Components")]
    public Components Components { get; set; }
}

public class Root {
    [JsonPropertyName("GameVersion")]
    public string GameVersion { get; set; }

    [JsonPropertyName("Timestamp")]
    public string Timestamp { get; set; }

    [JsonPropertyName("Singletons")]
    public Singletons Singletons { get; set; }

    [JsonPropertyName("Entities")]
    public List<Entity> Entities { get; set; }
}

