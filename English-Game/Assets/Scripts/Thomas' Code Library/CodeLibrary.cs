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

    public static void ResizeBoxColliderToMeshFilter(BoxCollider collider, MeshFilter mesh)
    {
        collider.size = mesh.mesh.bounds.size;
        collider.center = mesh.mesh.bounds.center;
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
}
