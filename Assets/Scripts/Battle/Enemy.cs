using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy1
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string maxWound1 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string maxWound2 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> actions { get; set; }
    
}
public class Enemy2
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string maxWound1 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string maxWound2 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> actions { get; set; }
}
public class Enemy3
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string maxWound1 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string maxWound2 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> actions { get; set; }
}
public class Enemy : ScriptableObject
{
    /// <summary>
    /// 
    /// </summary>
    public List<Enemy1> enemy1 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<Enemy2> enemy2 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<Enemy3> enemy3 { get; set; }
}
