using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CodeLibrary : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //All kinds of useful, and less useful code things

    public static Vector3 MaxVector3Size()
    {
        return new Vector3 { x = 10000, y = 10000, z = 10000 };
    }

    public static void RemoveParents(Transform transformInput)
    {
        foreach (Transform transform in transformInput)
        {
            RemoveParents(transform);
            transform.parent = null;
        }
    }

    public static Vector3 ScaleVector3Sum(Vector3 input, float sum)
    {
        Vector3 output;
        output.x = input.x + sum;
        output.y = input.y + sum;
        output.z = input.z + sum;
        return output;
    }

    public static Vector3 ScaleVector3Modifier(Vector3 input, float modifier)
    {
        Vector3 output;
        output.x = input.x * modifier;
        output.y = input.y * modifier;
        output.z = input.z * modifier;
        return output;
    }

    public static Vector3 ClampVector3(Vector3 input, Vector3 min, Vector3 max)
    {
        Vector3 output;
        output.x = Mathf.Clamp(input.x, min.x, max.x);
        output.y = Mathf.Clamp(input.y, min.y, max.y);
        output.z = Mathf.Clamp(input.z, min.z, max.z);
        return output;
    }

    /// <summary>
    /// Replaces the Game Object with another Game Object, with the same velocity and positition.
    /// </summary>
    /// <param name="replace"></param>
    /// <param name="with"></param>
    public static void ReplaceObject(GameObject replace, GameObject with)
    {
        GameObject tempObject = Instantiate(with, replace.transform.position, replace.transform.rotation);
        if (replace.GetComponent<Rigidbody>() == false) return;
        if (tempObject.GetComponent<Rigidbody>() == false) tempObject.AddComponent<Rigidbody>();
        MatchSpeed(tempObject.GetComponent<Rigidbody>(), replace.GetComponent<Rigidbody>());
        Destroy(replace);
    }

    public static void MatchSpeed(Rigidbody subject, Rigidbody matchedTo)
    {
        subject.velocity = matchedTo.velocity;
        subject.angularVelocity = matchedTo.angularVelocity;
    }

    public static void ResizeBoxColliderToMeshFilter(BoxCollider collider, MeshFilter mesh)
    {
        collider.size = mesh.mesh.bounds.size;
        collider.center = mesh.mesh.bounds.center;
    }

    public static void IncrementalIncrease(ref int input, int increment, int max, int min = 0)
    {
        input += increment;
        if (input > max) input = min;
        if (input < min) input = max;
    }

    public static void ResizeSphereColliderToMesh(SphereCollider collider, MeshFilter mesh)
    {
        collider.radius = GetHighestOfVector3(mesh.mesh.bounds.extents);
        collider.center = mesh.mesh.bounds.center;
    }

    public static float GetHighestOfArray(float[] array)
    {
        float output = 0;
        foreach (float floatyNumber in array)
        {
            SetIfHigher(ref output, floatyNumber);
        }
        return output;
    }

    public static float GetHighestOfVector3(Vector3 input)
    {
        float output = input.x;
        SetIfHigher(ref output, input.y);
        SetIfHigher(ref output, input.z);
        return output;
    }

    public static bool SetIfHigher(ref float var, float higherThan)
    {
        if (var > higherThan)
        {
            var = higherThan;
            return true;
        }
        return false;
    }

    public static void SetX(Transform transform, float newX)
    {
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    public static void SetY(Transform transform, float newY)
    {
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    public static void SetZ(Transform transform, float newZ)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }

    public static void AddX(Transform transform, float newX)
    {
        transform.position = new Vector3(newX + transform.position.x, transform.position.y, transform.position.z);
    }

    public static void AddY(Transform transform, float newY)
    {
        transform.position = new Vector3(transform.position.x, newY + transform.position.y, transform.position.z);
    }

    public static void AddZ(Transform transform, float newZ)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ + transform.position.z);
    }

    public static bool CheckForSameBools(bool[] booleans, bool whatToLookFor)
    {
        int multipleCount = 0;
        foreach (bool boolyBit in booleans)
        {
            if (boolyBit == whatToLookFor)
            {
                multipleCount++;
                if (multipleCount == 2)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static void SetVelocity(Rigidbody rb, float x = 0, float y = 0, float z = 0)
    {
        rb.velocity = new Vector3(x, y, z);
    }

    public static float ReturnBiggest(float input1, float input2)
    {
        if (input1 > input2)
        {
            return input1;
        }
        return input2;
    }

    public static float ReturnSmallest(float input1, float input2)
    {
        if (input1 < input2)
        {
            return input1;
        }
        return input2;
    }

    public static bool CheckSameObjectType(object input1, object input2)
    {
        if (input1.GetType() == input2.GetType())
        {
            return true;
        }
        return false;
    }

    public static bool BetweenTwoValues(float input, float min, float max)
    {
        if (input < min && input > max)
        {
            return false;
        }
        return true;
    }

    public static bool ContainsObject(object[] input, object checkFor)
    {
        if (input.GetType() != checkFor.GetType()) return false;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == checkFor) return true;
        }
        return false;
    }

    public static Color ConvertToTransparent(Color input, float alpha)
    {
        Color newColor = input;
        newColor.a = alpha;
        return newColor;
    }
}
