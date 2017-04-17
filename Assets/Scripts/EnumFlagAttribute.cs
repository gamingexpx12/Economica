using UnityEngine;
/// <summary>
/// Use this to make an enum into a collection of flags.
/// 
/// Example enum:
///     [Flags] 
///     Public Enum CanBeCompletedBy 
///     {
///         Maya = 1,
///         Phoenix = 2,
///         Apollo = 4
///     }
/// 
/// </summary>
class EnumFlagAttribute : PropertyAttribute
{
    public string enumName;

    public EnumFlagAttribute() { }

    public EnumFlagAttribute(string name)
    {
        enumName = name;
    }
}
