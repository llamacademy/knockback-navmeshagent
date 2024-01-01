# Knockback NavMesh Agents (AI Series 51)

In this tutorial repository and [accompanying video](https://youtu.be/0NH5obeOb7I) you will learn how to knock back NavMeshAgents using Physics & Forces!

[![Youtube Tutorial](./Video%20Screenshot.jpg)](https://youtu.be/0NH5obeOb7I)

## Quickstart
This repo uses the [Scriptable Object Gun System](https://github.com/llamacademy/scriptable-object-based-guns) as of branch `part-14` to handle shooting. 
This was then modified to add knockback. There's nothing specific to this system _required_ to make the knockback effect work. It just was a launching point to allow this to focus on the knockback instead of implementing a bunch of systems from scratch.

1. Import [Unity Particle Pack](https://assetstore.unity.com/packages/vfx/particles/particle-pack-127325?aid=1101l9QvC) (free)
2. Click play!

Everything should be working from there.

## Key Classes
While I highly recommend you check out the video for a complete understanding, I also understand that's not everybody's cup-of-tea. 
Here are the key classes if you just want to explore:
1. `IKnockbackable.cs` - Interface to allow arbitrary objects to be knocked back
2. `EnemyMovement.cs` - Implements `IKnockbackable` to handle how to get knocked back.
3. `KnockbackOnCollision.cs` - Applies force to `IKnockbackable`s `OnCollisionEnter`
4. `GunScriptableObject.cs` - Applies force to `IKnockbackable`s when bullets make impact
5. `KnockbackConfigScriptableObject.cs` - Houses the configuration for the knockback effect as well as calculating the 

## Sponsors
This tutorial is Sponsored by Southern New Hampshire University. If you're interested in leveling up your gamedev skillset, consider getting an affordable, accredited degree in Computer Science or Game Design & Development from SNHU. You can go to https://snhu.edu/llamacademy to see what the current average annual salary for a game developer programmer is and learn how you can get started!

## Supporters
Have you been getting value out of these tutorials? Do you believe in LlamAcademy's mission of helping everyone make their game dev dream become a reality? Consider becoming a Patreon supporter and get your name added to this list, as well as other cool perks.
Head over to the [LlamAcademy Patreon Page](https://patreon.com/llamacademy), join as a [YouTube Member](https://www.youtube.com/channel/UCnWm6pMD38R1E2vCAByGb6w/join), or even become a [GitHub Sponsor](https://github.com/sponsors/llamacademy) to show your support.

### Phenomenal Supporter Tier
* YOUR NAME HERE!

### Tremendous Supporter Tier
* YOUR NAME HERE!

### Awesome Supporter Tier
* Ivan
* Reulan
* Iffy Obelus
* Perry
* Mustafa
* YOUR NAME HERE!

### Supporters
* Trey Briggs
* Matt Sponholz
* Dr Bash
* Tarik
* Sean
* Elijah Singer
* Bruno Bozic
* Josh Meyer
* Ewald Schulte
* Andrew Allbright
* AudemKay x2
* Claduiu Barsan-Pipu
* Ben
* Christian Van Sttenwijk
* Joseph Janosko
* Chimera Dev
* Wendy Whitner
* Lukas Wolfe
* YOUR NAME HERE!

## Other Projects
Interested in other Topics in Unity? 

* [Check out the LlamAcademy YouTube Channel](https://youtube.com/c/LlamAcademy)!
* [Check out the LlamAcademy GitHub for more projects](https://github.com/llamacademy)

## Requirements
* Requires Unity 2022.3 LTS or higher.
* Universal Render Pipeline
* Unity Particle Pack

## Disclaimers
Some links in this readme may contain affiliate links which, at the time of purchase, at no additional cost to you, gives me a small portion of the purchase.