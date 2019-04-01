using System;
using UnityEngine;

public struct AppVersion
{
    #region Contructor


    /// <summary>
    /// Creates AppVersion struct from string representing version and build number
    /// </summary>
    /// <param name="version">Get it from PlayerSettings.bundleVersion</param>
    /// <param name="build">Get it from PlayerSettings.macOS.buildNumber</param>
    public AppVersion(string version, string build)
    {
        var split = version.Split('.');
        Release = int.Parse(split[0]);
        Major = int.Parse(split[1]);
        Minor = int.Parse(split[2]);
        Build = int.Parse(build);
    }
    #endregion

    #region Properties
    public int Release { get; private set; }
    public int Major { get; private set; }
    public int Minor { get; private set; }
    public int Build { get; private set; }
    #endregion

    #region Convert
    public enum VersionMode
    {
        FULL,
        RELEASEMAJORMINOR,
        BUILD
    }
    public string ConvertToString(VersionMode format)
    {
        string version = $"{Release}.{Major}.{Minor}";

        switch (format)
        {
            case VersionMode.FULL:
                return ($"{version}.{Build}");
            case VersionMode.RELEASEMAJORMINOR:
                return version;
            case VersionMode.BUILD:
                return Build.ToString();
            default:
                throw new ArgumentOutOfRangeException(nameof(format), format, null);
        }

    }
    #endregion

    #region Increment
    public void IncrementBuild()
    {
        this.Build = this.Build + 1;
    }
    #endregion

    #region Print
    public void print(VersionMode format = VersionMode.FULL)
    {
        Debug.Log(this.ConvertToString(format));
    }
    #endregion
}
