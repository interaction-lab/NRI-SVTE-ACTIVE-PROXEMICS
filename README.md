# NRI-SVTE
This repository holds the history for 2 projects. The first was augmented reality (AR) robot capability visualizations for non-expert users:

```
@inproceedings{groechel2022reimagining,
  title={Reimagining RViz: Multidimensional Augmented Reality Robot Signal Design},
  author={Groechel, Thomas R and O’Connell, Amy and Nigro, Massimiliano and Matari{\'c}, Maja J},
  booktitle={2022 31st IEEE International Conference on Robot and Human Interactive Communication (RO-MAN)},
  pages={1224--1231},
  year={2022},
  organization={IEEE}
}
```
Study commit: [77610bae](https://github.com/interaction-lab/NRI-SVTE/commit/77610bae15ae9a2fb632c31f9b26150ffe8e258a), [Video Presentation](https://youtu.be/Xw2_kHyN-xA).

The second is a project using active learning for personlized proxemics within an AR Human-Robot Interaction with older adults. This work is currently being submitted. Study commit: [9a1fd7e9](https://github.com/interaction-lab/NRI-SVTE-ACTIVE-PROXEMICS/commit/9a1fd7e946913fe923190b5444845061c74462c3)

## Thanks
We want to thank 
- [The Kiwi Coder](https://thekiwicoder.com/behaviour-tree-editor/) for their free behavior tree unitypackage.
- [KDTrees](https://github.com/viliwonka/KDTree) for their package on kd trees
## Setup

- Change line 88 of `ColorPicker.cs` to 
    ```
    public Color CustomColor;
    ```
- Change lines 1872-1873 in `BoundingBox.cs` and lines 905-906 in `BoundsControl.cs` from
    ```
    KeyValuePair<Transform, Collider> colliderByTransform;
    KeyValuePair<Transform, Bounds> rendererBoundsByTransform;
    ``` 
    to
    ```
    KeyValuePair<Transform, Collider> colliderByTransform = default;
    KeyValuePair<Transform, Bounds> rendererBoundsByTransform = default;
    ```

## Style Guide

- Follow Microsoft's [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
