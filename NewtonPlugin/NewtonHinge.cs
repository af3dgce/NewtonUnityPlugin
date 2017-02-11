﻿using System;
using UnityEngine;
using System.Runtime.InteropServices;


[AddComponentMenu("Newton Physics/Joints/Hinge")]
public class NewtonHinge : NewtonJoint
{
    public override void Create()
    {
        //Matrix4x4 bodyMatrix = Matrix4x4.identity;
        Matrix4x4 localMatrix = Matrix4x4.identity;

        //bodyMatrix.SetTRS(transform.position, transform.rotation, Vector3.one);
        localMatrix.SetTRS(m_posit, Quaternion.Euler(m_rotation), Vector3.one);

        // I do not understand why I get the same matrix either way, I am doing the matrix multiplication wrong
        //Matrix4x4 matrix = localMatrx * bodyMatrix;

        IntPtr floatsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(localMatrix));
        Marshal.StructureToPtr(localMatrix, floatsPtr, false);
        NewtonBody child = GetComponent<NewtonBody>();
        if (m_otherBody == null)
        {
            m_joint = new dNewtonHinge(floatsPtr, child.GetBody().GetBody());
        }
        else
        {
            m_joint = new dNewtonHinge(floatsPtr, child.GetBody().GetBody(), m_otherBody.GetBody().GetBody());
        }
    }


    void OnDrawGizmosSelected()
    {
        Matrix4x4 bodyMatrix = Matrix4x4.identity;
        Matrix4x4 localMatrix = Matrix4x4.identity;
        bodyMatrix.SetTRS(transform.position, transform.rotation, Vector3.one);
        localMatrix.SetTRS(m_posit, Quaternion.Euler(m_rotation), Vector3.one);

        Gizmos.color = Color.red;

        Gizmos.matrix = bodyMatrix;
        Vector3 direction = localMatrix.MultiplyPoint3x4(new Vector3(1.0f, 0.0f, 0.0f));
        Gizmos.DrawLine(m_posit, direction);
    }

    public Vector3 m_posit = Vector3.zero;
    public Vector3 m_rotation = Vector3.zero;
}




