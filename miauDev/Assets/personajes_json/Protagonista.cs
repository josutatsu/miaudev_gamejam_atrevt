using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public string nombre;
    public int cantidad;
}

[Serializable]
public class Protagonista
{
    public string nombre;
    public int cordura;
    public float salud;
    public float percepcion;

    public Vector3 posicion;

    public List<Item> inventario = new List<Item>();
}
